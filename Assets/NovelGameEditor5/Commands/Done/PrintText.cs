// 日本語対応
using Cysharp.Threading.Tasks;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

public class PrintText : ICommand
{
    private string _text;
    private float _interval;
    private StringBuilder _stringBuilder = new StringBuilder();

    private TextMeshProUGUI View => NovelData.Current.Text;

    public PrintText(string[] parametorData)
    {
        _text = parametorData[0];
        _interval = float.Parse(parametorData[1]);
    }

    public async UniTask RunCommand(CancellationToken token = default)
    {
        await ShowTextAsync(_text, _interval);
    }

    public async UniTask ShowTextAsync(string text, float interval)
    {
        int index = -1;
        float elapsed = 0f;
        ClearMessage();
        while (index < text.Length - 1)
        {
            await UniTask.Yield();
            if (GameSpeedController.Instance.IsPaused) continue;

            elapsed += Time.deltaTime;
            if (elapsed >= interval)
            {
                elapsed -= interval;
                index++;
                AppendCharacter(text[index]);
            }

            if (StepTrigger())
            {
                await UniTask.Yield();
                break;
            }
        }
        ShowText(text);
        await WaitStep();
        await UniTask.Yield();
    }

    public void ShowText()
    {
        View.text = _stringBuilder.ToString();
    }

    public void ShowText(string text)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(text);
        View.text = text;
    }

    public void AppendCharacter(char c)
    {
        _stringBuilder.Append(c);
        ShowText();
    }

    public void ClearMessage()
    {
        _stringBuilder.Clear();
        ShowText();
    }

    private bool StepTrigger()
    {
        if (GameSpeedController.Instance.IsPaused) return false;
        return Input.GetKeyDown(KeyCode.Space);
    }

    private async UniTask WaitStep()
    {
        _stepTimer = 0f;
        AutoModeManager.OnAutoModeStarted += ClearAutoTimer;
        while (_stepTimer < NovelData.Current.AutoDuration)
        {
            if (AutoModeManager.IsAutoMode)
            {
                _stepTimer += Time.deltaTime * GameSpeedController.GameSpeed;
            }

            if (StepTrigger())
            {
                break;
            }
            await UniTask.Yield();
        }
        AutoModeManager.OnAutoModeStarted -= ClearAutoTimer;
        await UniTask.Yield();
    }

    private float _stepTimer = 0f;

    private AutoModeManager AutoModeManager => AutoModeManager.Instance;
    private GameSpeedController GameSpeedController => GameSpeedController.Instance;

    private void ClearAutoTimer()
    {
        _stepTimer = 0f;
    }
}
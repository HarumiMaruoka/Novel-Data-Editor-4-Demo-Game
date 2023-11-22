using Cysharp.Threading.Tasks;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplayer
{
    public MessageDisplayer(Text view)
    {
        _view = view;
        _stringBuilder = new StringBuilder();
    }

    private Text _view;
    private StringBuilder _stringBuilder;

    public async UniTask DisplayMessageAnimation(string message, float interval, CancellationToken token)
    {
        int index = -1;
        float elapsed = 0f;
        while (index < message.Length - 1)
        {
            elapsed += Time.deltaTime; // TODO: ゲームスピード掛ける
            if (elapsed >= interval)
            {
                elapsed -= interval;
                index++;
                AppendCharacter(message[index]);
            }

            if (Input.GetKeyDown(KeyCode.Space)) break;

            await UniTask.Yield(token);
        }
        ShowMessage(message);
    }

    public void ShowMessage()
    {
        _view.text = _stringBuilder.ToString();
    }

    public void ShowMessage(string message)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(message);
        _view.text = message;
    }

    public void AppendCharacter(char c)
    {
        _stringBuilder.Append(c);
        ShowMessage();
    }

    public void ClearMessage()
    {
        _stringBuilder.Clear();
        ShowMessage();
    }

    public void HideMessage()
    {
        _view.text = null;
    }
}
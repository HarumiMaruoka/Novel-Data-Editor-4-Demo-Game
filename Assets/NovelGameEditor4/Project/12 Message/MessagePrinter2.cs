// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrinter2 : Glib.NovelGameEditor.NovelAnimationBehavior
{
    [SerializeField]
    private Text _view;
    [SerializeField]
    private float _interval = 0.1f;
    [SerializeField, Range(0.2f, 10f)]
    private float _autoStepDuration = 1f;

    [SerializeField]
    private int _sheetID;
    [SerializeField]
    private string _sheetName;
    [SerializeField]
    private int _ignoreRows = 0;

    private Message[] _messages = null;
    private MessageDisplayer _messageDisplayer;

    private LogContainer LogContainer => LogManager.Instance.Container;

    private async void Start()
    {
        _messageDisplayer = new MessageDisplayer(_view);
        var requestData = new MessageLoader.RequestData(_sheetID, _sheetName, _ignoreRows);

        _messages = await MessageLoader.LoadMessagesAsync(requestData, this.GetCancellationTokenOnDestroy());
    }

    public override async UniTask PlayAnimationAsync()
    {
        if (!this) return;
        try
        {
            if (_messages == null)
            {
                try
                {
                    await UniTask.WaitUntil(() => _messages != null, cancellationToken: this.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    Debug.Log("canceled...");
                    return;
                }
            }

            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.Yield(token);
            foreach (var message in _messages)
            {
                await _messageDisplayer.ShowMessageAnimation(message.Text, _interval, token);
                ClearAutoTimer();
                await UniTask.WaitUntil(StepTrigger, cancellationToken: token);
                LogContainer.Add(message);
            }
            await UniTask.Yield(token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("canceled...");
        }
    }

    private bool StepTrigger()
    {
        if (GameSpeedController.Instance.IsPaused) return false;
        return IsAutoStep || Input.GetKeyDown(KeyCode.Space);
    }

    #region Auto
    private float _stepTimer = 0f;
    private bool IsAutoStep => _stepTimer > _autoStepDuration;

    private AutoModeManager AutoModeManager => AutoModeManager.Instance;
    private GameSpeedController GameSpeedController => GameSpeedController.Instance;

    private void Update()
    {
        if (AutoModeManager.IsAutoMode)
        {
            _stepTimer += Time.deltaTime * GameSpeedController.GameSpeed;
        }
    }

    private void OnEnable()
    {
        AutoModeManager.OnAutoModeStarted += ClearAutoTimer;
    }

    private void OnDisable()
    {
        AutoModeManager.OnAutoModeStarted -= ClearAutoTimer;
    }

    private void ClearAutoTimer()
    {
        _stepTimer = 0f;
    }
    #endregion
}
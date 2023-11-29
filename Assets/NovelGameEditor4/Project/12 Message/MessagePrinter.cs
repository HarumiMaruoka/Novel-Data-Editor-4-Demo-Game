// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrinter : Glib.NovelGameEditor.NovelAnimationBehavior
{
    [SerializeField]
    private Text _view;
    [SerializeField]
    private Message[] _messages;
    [SerializeField]
    private float _interval = 0.1f;

    private MessageDisplayer _messageDisplayer;
    private LogContainer LogContainer => LogManager.Instance.Container;

    private void Awake()
    {
        _messageDisplayer = new MessageDisplayer(_view);
    }

    public override async UniTask PlayAnimationAsync()
    {
        try
        {
            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.Yield(token);
            foreach (var message in _messages)
            {
                await _messageDisplayer.ShowMessageAnimation(message.Text, _interval, token);
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
        return Input.GetKeyDown(KeyCode.Space);
    }
}
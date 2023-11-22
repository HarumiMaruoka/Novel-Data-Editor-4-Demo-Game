// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
using System;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrinter2 : Glib.NovelGameEditor.NovelAnimationBehavior
{
    [SerializeField]
    private Text _view;
    [SerializeField, TextArea(0, 100)]
    private string _message;
    [SerializeField]
    private float _interval = 0.1f;

    private MessageDisplayer _sample;
    private void Awake()
    {
        _sample = new MessageDisplayer(_view);
    }

    public override async UniTask PlayAnimationAsync(NovelAnimationData animationData)
    {
        try
        {
            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.Yield(token);
            await _sample.DisplayMessageAnimation(_message, _interval, token);

            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: token);
            await UniTask.Yield(token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("canceled...");
        }
    }
}
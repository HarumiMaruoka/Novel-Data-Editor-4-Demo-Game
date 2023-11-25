// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Fade : NovelAnimationBehavior
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private FadeMode _mode;
    [SerializeField, Range(0.1f, 2f)]
    private float _duration;

    [SerializeField]
    private bool _playOnAwake = false;

    private async void Awake()
    {
        if (_playOnAwake) await PlayAnimationAsync();
    }

    public override async UniTask PlayAnimationAsync()
    {
        if (_mode == FadeMode.FadeIn)
            await FadeIn();
        else
            await FadeOut();
    }

    public async UniTask FadeIn(CancellationToken token = default)
    {
        _image.enabled = true;
        var col = _image.color;
        col.a = 1f;

        try
        {
            await _image.FadeAsync(_image.color, col, _duration, token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("canceled...");
            return;
        }
    }

    public async UniTask FadeOut(CancellationToken token = default)
    {
        _image.enabled = true;
        var col = _image.color;
        col.a = 0f;

        try
        {
            await _image.FadeAsync(_image.color, col, _duration, token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("canceled...");
            return;
        }
        _image.enabled = false;
    }

    public enum FadeMode
    {
        FadeIn,
        FadeOut,
    }
}
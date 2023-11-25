// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
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

    public override async UniTask PlayAnimationAsync()
    {
        if (_mode == FadeMode.FadeIn)
            await FadeIn();
        else
            await FadeOut();
    }

    public async UniTask FadeIn()
    {
        _image.enabled = true;
        var col = _image.color;
        col.a = 0f;

        await _image.FadeInAsync(_duration);
        _image.enabled = false;
    }

    public async UniTask FadeOut()
    {
        _image.enabled = true;
        var col = _image.color;
        col.a = 1f;

        await _image.FadeOutAsync(_duration);
        _image.enabled = false;
    }

    public enum FadeMode
    {
        FadeIn,
        FadeOut,
    }
}
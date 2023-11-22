// 日本語対応
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Glib.NovelGameEditor;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Fade : NovelAnimationBehavior
{
    [SerializeField]
    private Image _image;
    [SerializeField, Range(0f, 1f)]
    private float _endValue;
    [SerializeField, Range(0.1f, 2f)]
    private float _duration;

    public override async UniTask PlayAnimationAsync(NovelAnimationData animationData)
    {
        await _image.DOFade(_endValue, _duration);
    }
}
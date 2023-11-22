// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrinter : Glib.NovelGameEditor.NovelAnimationBehavior
{
    [SerializeField]
    private Text _view;

    [SerializeField]
    private string _text;

    public override async UniTask PlayAnimationAsync(NovelAnimationData animationData)
    {
        _view.text = _text;
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        await UniTask.Yield();
    }
}
// 日本語対応
using Cysharp.Threading.Tasks;
using Glib.NovelGameEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnityAnimationPlayer : NovelAnimationBehavior
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _animationName;

    [SerializeField]
    private Text text;

    public async override UniTask PlayAnimationAsync()
    {
        if (_animator == null)
        {
            Debug.LogError("Animator reference is missing!");
            return;
        }

        if (string.IsNullOrEmpty(_animationName))
        {
            Debug.LogError("Animation name is not specified!");
            return;
        }

        // アニメーションを再生
        _animator.Play(_animationName);

        // アニメーションの長さを取得して待機
        await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        // アニメーション再生終了後の処理
        // Debug.Log("Animation finished!");
    }

    //private void Update()
    //{
    //    if (_animator != null)
    //    {
    //        text.text = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime.ToString("00.00");
    //    }
    //}
}
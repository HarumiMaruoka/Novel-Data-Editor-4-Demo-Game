// 日本語対応
using Glib.NovelGameEditor;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class AnimationSequencer : NovelAnimationBehavior
{
    [SerializeField]
    private AnimationBehaviorArray[] _novelAnimationBehavior;

    public async override UniTask PlayAnimationAsync()
    {
        for (int i = 0; i < _novelAnimationBehavior.Length; i++)
        {
            var animationGroup = _novelAnimationBehavior[i];

            if (animationGroup == null || animationGroup.Animations.Length == 0)
            {
                Debug.Log("animation is none");
                continue;
            }

            UniTask[] animationTasks = new UniTask[animationGroup.Animations.Length];

            // アニメーションを並列再生
            for (int j = 0; j < animationGroup.Animations.Length; j++)
            {
                animationTasks[j] = animationGroup.Animations[j].PlayAnimationAsync();
            }

            // 全てのアニメーションが正常に終了するか、キャンセルされるまで待機
            try
            {
                await UniTask.WhenAll(animationTasks);
                continue; // すべてのアニメーションが正常に終了した場合
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Canceled");
                return; // キャンセルされた場合
            }
        }
    }

    [Serializable]
    private class AnimationBehaviorArray // 2次元配列をインスペクタに描画するための小技
    {
        [SerializeField]
        private NovelAnimationBehavior[] _animations;

        public NovelAnimationBehavior[] Animations => _animations;
    }
}
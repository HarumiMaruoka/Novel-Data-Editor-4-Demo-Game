using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    [Serializable]
    public class NovelAnimationController
    {
        [SerializeField]
        private NovelAnimationBehavior[] _animations;

        public void OnEnter(AnimationNode node)
        {
            foreach (var animation in _animations)
            {
                animation.OnEneter(node);
            }
        }

        public void OnExit(AnimationNode node)
        {
            foreach (var animation in _animations)
            {
                animation.OnExit(node);
            }
        }

        public async UniTask<bool> PlayAnimation()
        {
            if (_animations == null || _animations.Length == 0)
            {
                Debug.Log("animation is none");
                return false;
            }

            UniTask[] animationTasks = new UniTask[_animations.Length];

            // アニメーションを並列再生
            for (int i = 0; i < _animations.Length; i++)
            {
                animationTasks[i] = _animations[i].PlayAnimationAsync();
            }

            // 全てのアニメーションが正常に終了するか、キャンセルされるまで待機
            try
            {
                await UniTask.WhenAll(animationTasks);
                return true; // すべてのアニメーションが正常に終了した場合
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Canceled");
                return false; // キャンセルされた場合
            }
        }
    }
}
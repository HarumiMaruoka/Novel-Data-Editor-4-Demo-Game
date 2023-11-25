// 日本語対応
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public abstract class NovelAnimationBehavior : MonoBehaviour
    {
        public abstract UniTask PlayAnimationAsync();

        public virtual void OnEneter(AnimationNode node)
        {

        }

        public virtual void OnExit(AnimationNode node)
        {

        }
    }
}
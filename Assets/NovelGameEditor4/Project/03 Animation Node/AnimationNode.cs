// 日本語対応
using System.Collections.Generic;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class AnimationNode : Node, ISingleChild, IMultiParent
    {
        [SerializeField]
        private Node _child = null;
        [SerializeField]
        private List<Node> _parents = new List<Node>();
        [SerializeField]
        private NovelAnimationController _novelAnimation;

        public Node Node => this;
        public Node Child { get => _child; set => _child = value; }
        public List<Node> Parents => _parents;

        public void InputConnect(Node parent)
        {
            _parents.Add(parent);
        }

        public void InputDisconnect(Node parent)
        {
            _parents.Remove(parent);
        }

        public void OutputConnect(Node child)
        {
            _child = child;
        }

        public void OutputDisconnect(Node child)
        {
            _child = null;
        }

        public override async void OnEnter()
        {
            _novelAnimation.OnEnter(this);
            // 演出の再生が完了したら子ノードに遷移する。
            await _novelAnimation.PlayAnimation();
            _controller.MoveTo(_child);
        }

        public override void OnExit()
        {
            _novelAnimation.OnExit(this);
        }
    }
}
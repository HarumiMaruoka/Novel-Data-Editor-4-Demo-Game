// 日本語対応
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class BranchNode : Node, IMultiParent
    {
        [SerializeField]
        private List<Node> _parents = new List<Node>();
        [SerializeField]
        private List<BranchElement> _elements = new List<BranchElement>();
        [SerializeField]
        private NovelBranchBehaviour[] _behaviours;

        public Node Node => this;

        public IReadOnlyList<BranchElement> Elements => _elements;
        public List<Node> Parents => _parents;

        public void InputConnect(Node parent)
        {
            _parents.Add(parent);
        }

        public void InputDisconnect(Node parent)
        {
            _parents.Remove(parent);
        }

        public BranchElement CreateElement()
        {
            var instance = ScriptableObject.CreateInstance<BranchElement>();
            _elements.Add(instance);
            return instance;
        }

        public bool DeleteElement(BranchElement element)
        {
            return _elements.Remove(element);
        }

        public override void OnEnter()
        {
            foreach (var branchBehaviour in _behaviours)
            {
                branchBehaviour.OnEnter(this);
            }
        }

        public override void OnUpdate()
        {
            foreach (var branchBehaviour in _behaviours)
            {
                branchBehaviour.OnUpdate(this);
            }
        }

        public override void OnExit()
        {
            foreach (var branchBehaviour in _behaviours)
            {
                branchBehaviour.OnExit(this);
            }
        }

        public void MoveTo(Node node)
        {
            _controller.MoveTo(node);
        }
    }
}
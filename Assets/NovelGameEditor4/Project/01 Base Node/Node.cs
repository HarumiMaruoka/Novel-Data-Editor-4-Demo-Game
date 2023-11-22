using System;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class Node : ScriptableObject
    {
        [SerializeField]
        private NodeViewData _viewData;
        [SerializeField]
        private string _nodeName;

        protected NovelGameController _controller;

        public NodeViewData ViewData => _viewData ??= new NodeViewData();
        public string NodeName => _nodeName;
        public NovelGameController Controller => _controller;

        public void Initialize(NovelGameController controller)
        {
            _controller = controller;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}
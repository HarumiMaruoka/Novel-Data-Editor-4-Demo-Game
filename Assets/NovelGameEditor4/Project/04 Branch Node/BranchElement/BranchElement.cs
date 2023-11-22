// 日本語対応
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class BranchElement : Node, ISingleChild
    {
        [SerializeField]
        private Node _child;
        [SerializeField]
        private string _text;

        public string Text => _text;

        public Node Child { get => _child; set => _child = value; }

        public Node Node => this;

        public void OutputConnect(Node child)
        {
            _child = child;
        }

        public void OutputDisconnect(Node child)
        {
            _child = null;
        }
    }
}
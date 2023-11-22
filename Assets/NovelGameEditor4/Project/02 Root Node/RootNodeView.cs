// 日本語対応
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Glib.NovelGameEditor
{
    public class RootNodeView : NodeView, IOutputNodeView
    {
        public RootNodeView(RootNode node) : base(node)
        {
            _node = node;
            title = "Root Node";
        }

        private RootNode _node;

        public Port OutputPort => _output;

        public IOutputNode OutputConnectable => _node;
    }
}
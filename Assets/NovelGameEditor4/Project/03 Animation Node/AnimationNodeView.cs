// 日本語対応
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Glib.NovelGameEditor
{
    public class AnimationNodeView : NodeView, IInputNodeView, IOutputNodeView
    {
        public AnimationNodeView(AnimationNode node, Vector2 initialPos) : base(node, NovelEditorWindow.FindUxml("AnimationNodeView"), initialPos)
        {
            _node = node;
            title = "Animation Node";
            _label = this.Q<Label>("node-name");
            _label.text = node.NodeName;
        }

        private Label _label;
        private AnimationNode _node;

        public Port OutputPort => _output;
        public Port InputPort => _input;

        public IOutputNode OutputConnectable => _node;
        public IInputNode InputConnectable => _node;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Glib.NovelGameEditor
{
    public class NovelNodeGraph : MonoBehaviour
    {
        [SerializeReference]
        private RootNode _rootNode;
        [SerializeReference]
        private List<Node> _nodes = new List<Node>();

        public RootNode RootNode { get => _rootNode; set => _rootNode = value; }
        public IReadOnlyList<Node> Nodes => _nodes;

        public Node CreateNode(Type type)
        {
            var instance = ScriptableObject.CreateInstance(type) as Node;
            if (instance == null) throw new ArgumentNullException(nameof(type));
            _nodes.Add(instance);
            return instance;
        }

        public void DeleteNode(Node node)
        {
            if (_rootNode == node) _rootNode = null;
            _nodes.Remove(node);
        }

        public void Connect(IOutputNodeView parent, IInputNodeView child)
        {
            parent.OutputConnectable.OutputConnect(child.InputConnectable.Node);
            child.InputConnectable.InputConnect(parent.OutputConnectable.Node);
        }

        public void Disconnect(IOutputNodeView parent, IInputNodeView child)
        {
            parent.OutputConnectable.OutputDisconnect(child.InputConnectable.Node);
            child.InputConnectable.InputDisconnect(parent.OutputConnectable.Node);
        }
    }
}
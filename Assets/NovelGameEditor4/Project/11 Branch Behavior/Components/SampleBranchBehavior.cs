// 日本語対応
using Glib.NovelGameEditor;
using UnityEngine;

public class SampleBranchBehavior : NovelBranchBehaviour
{
    [SerializeField]
    private SelectionManager _selectionManager;

    private BranchNode _branchNode;

    public override void OnEnter(BranchNode branchNode)
    {
        _branchNode = branchNode;
        _selectionManager.ShowSelections(branchNode.Elements);
        _selectionManager.OnSelected += OnSelected;
    }

    public override void OnExit(BranchNode branchNode)
    {
        _selectionManager.HideSelections();
        _selectionManager.OnSelected -= OnSelected;
    }

    private void OnSelected(BranchElement select)
    {
        _branchNode.Controller.MoveTo(select.Child);
    }
}
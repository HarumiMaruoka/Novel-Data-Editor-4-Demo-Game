// 日本語対応
using Glib.NovelGameEditor;
using UnityEngine;

public class SampleBranchBehavior : NovelBranchBehaviour
{
    private int _index = 0;

    public override void OnUpdate(BranchNode branchNode)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _index++;
            if (_index == branchNode.Elements.Count) _index = 0;
            Debug.Log($"Index: {_index}");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _index--;
            if (_index == -1) _index = branchNode.Elements.Count - 1;
            Debug.Log($"Index: {_index}");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (branchNode.Elements[_index].Child != null)
                branchNode.Controller.MoveTo(branchNode.Elements[_index].Child);
        }
    }
}
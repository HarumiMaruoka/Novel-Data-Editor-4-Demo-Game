// 日本語対応
using Glib.NovelGameEditor;
using UnityEngine;

public class NovelBranchBehaviour : MonoBehaviour
{
    public virtual void OnEnter(BranchNode branchNode) { }
    public virtual void OnUpdate(BranchNode branchNode) { }
    public virtual void OnExit(BranchNode branchNode) { }
}
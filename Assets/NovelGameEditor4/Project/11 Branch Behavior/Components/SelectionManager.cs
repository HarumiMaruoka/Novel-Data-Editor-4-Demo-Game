// 日本語対応
using Glib.NovelGameEditor;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private Transform _selectionViewParent;
    [SerializeField]
    private SelectionView _selectionViewPrefab;

    private Stack<SelectionView> _alives = new Stack<SelectionView>();
    private Stack<SelectionView> _disposed = new Stack<SelectionView>();



    public void ShowSelections(BranchElement[] branchElements)
    {
        foreach (var branchElement in branchElements)
        {
            SelectionView instance = null;
            if (_disposed.Count != 0)
            {
                instance = _disposed.Pop();
            }
            else
            {
                instance = Instantiate(_selectionViewPrefab, _selectionViewParent);
            }

            instance.OnShow();
            instance.ApplyText(branchElement.Text);

            _alives.Push(instance);
        }
    }

    public void HideSelections()
    {
        while (_alives.Count > 0)
        {
            var dispose = _alives.Pop();
            dispose.OnHide();
            _disposed.Push(dispose);
        }
    }
}
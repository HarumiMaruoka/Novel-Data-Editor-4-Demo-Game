// 日本語対応
using Glib.NovelGameEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SelectionManager
{
    [SerializeField]
    private Transform _selectionViewParent;
    [SerializeField]
    private SelectionView _selectionViewPrefab;

    private Stack<SelectionView> _alives = new Stack<SelectionView>();
    private Stack<SelectionView> _disposed = new Stack<SelectionView>();

    public event Action<BranchElement> OnSelected;

    public void ShowSelections(IEnumerable<BranchElement> branchElements)
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
                instance = GameObject.Instantiate(_selectionViewPrefab, _selectionViewParent);
            }

            instance.OnSelected += OnSelect;
            instance.OnShow();
            instance.Initialize(branchElement);

            _alives.Push(instance);
        }
    }

    public void HideSelections()
    {
        while (_alives.Count > 0)
        {
            var dispose = _alives.Pop();
            dispose.OnHide();
            dispose.Dispose();
            _disposed.Push(dispose);
        }
    }

    private void OnSelect(SelectionView selectionView)
    {
        OnSelected?.Invoke(selectionView.Selection);
    }
}
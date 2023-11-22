// 日本語対応
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class SelectionView : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private int _index;

    public int Index => _index;

    public event Action<SelectionView> OnSelected;

    public void Initialize(int index)
    {
        _index = index;
    }

    public void ApplyText(string text)
    {
        _text.text = text;
    }

    public virtual void OnShow()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    protected virtual void Update()
    {
        if (Input.GetButtonDown($"Alpha{_index}"))
        {
            OnSelected?.Invoke(this);
        }
    }

    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }
}
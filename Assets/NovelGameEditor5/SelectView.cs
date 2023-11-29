// 日本語対応
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI _textView;

    public TextMeshProUGUI TextView => _textView;

    public event Action<SelectView> OnSelected;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelected?.Invoke(this);
    }
}
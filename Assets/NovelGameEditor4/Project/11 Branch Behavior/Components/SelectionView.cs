// 日本語対応
using Glib.NovelGameEditor;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Text _textView;
    [SerializeField]
    private Image _image;

    public event Action<SelectionView> OnSelected;

    private BranchElement _selection;
    public BranchElement Selection => _selection;

    public void Initialize(BranchElement selection)
    {
        _selection = selection;
        _textView.text = _selection.Text;
    }

    public void Dispose()
    {
        OnSelected = null;
        _selection = null;
    }

    public virtual void OnShow()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    protected virtual void Update()
    {

    }

    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnSelected?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = Color.gray;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = Color.red;
    }
}
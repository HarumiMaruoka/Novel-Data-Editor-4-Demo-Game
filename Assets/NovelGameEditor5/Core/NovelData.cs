// 日本語対応
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class NovelData : MonoBehaviour
{
    private static NovelData _current;
    public static NovelData Current => _current;

    private void Awake()
    {
        _current = this;
    }
    private void OnDestroy()
    {
        _current = null;
    }

    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private Image _screenImage;
    [SerializeField]
    private Transform _selectViewPrefabParent;
    [SerializeField]
    private SelectView _selectViewPrefab;
    [SerializeField, Range(0.8f, 2f)]
    private float _autoDuration = 1f;

    public TextMeshProUGUI Text => _text;
    public Image ScreenImage => _screenImage;
    public Transform SelectViewPrefabParent => _selectViewPrefabParent;
    public SelectView SelectViewPrefab => _selectViewPrefab;
    public float AutoDuration => _autoDuration;
}
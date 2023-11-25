// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class LogView : MonoBehaviour
{
    [SerializeField]
    private Text _personName;
    [SerializeField]
    private Text _text;

    public Text PersonName => _personName;
    public Text Text => _text;
}
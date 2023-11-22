// 日本語対応
using UnityEngine;

public class Memopad : MonoBehaviour
{
    [SerializeField, TextArea(0, 1000)]
    private string _text;
}
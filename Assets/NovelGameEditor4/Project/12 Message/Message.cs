// 日本語対応
using UnityEngine;
using System;

[Serializable]
public struct Message
{
    public Message(string personName, string text)
    {
        _personName = personName;
        _text = text;
    }

    [SerializeField]
    private string _personName;
    [SerializeField, TextArea(1, 10)]
    private string _text;

    public string PersonName => _personName;
    public string Text => _text;
}
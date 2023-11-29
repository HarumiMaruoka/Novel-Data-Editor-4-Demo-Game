// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Selectable : ICommand
{
    private List<ICommand> _commands = new List<ICommand>();

    public List<ICommand> Commands => _commands;

    private string _text;
    private SelectView _view = null;

    public string Text => _text;

    private Transform ViewPrefabParent => NovelData.Current.SelectViewPrefabParent;
    private SelectView ViewPrefab => NovelData.Current.SelectViewPrefab;

    public Selectable(string[] parametorData)
    {
        _text = parametorData[0];
    }

    public async UniTask RunCommand(CancellationToken token = default)
    {
        if (_commands == null || _commands.Count == 0) return;

        var tasks = new UniTask[_commands.Count];
        for (int i = 0; i < _commands.Count; i++)
        {
            tasks[i] = _commands[i].RunCommand(token);
        }
        await UniTask.WhenAll(tasks);
    }

    public void Show()
    {
        _view = GameObject.Instantiate(ViewPrefab, ViewPrefabParent);
        _view.TextView.text = _text;
        _view.OnSelected += OnSelectedView;
    }

    public void Hide()
    {
        _view.OnSelected -= OnSelectedView;
        GameObject.Destroy(_view.gameObject);
        _view = null;
    }

    public event Action<Selectable> OnSelected;

    private void OnSelectedView(SelectView view)
    {
        OnSelected?.Invoke(this);
    }
}
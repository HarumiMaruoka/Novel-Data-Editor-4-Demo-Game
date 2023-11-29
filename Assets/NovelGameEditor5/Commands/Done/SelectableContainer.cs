// 日本語対応
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public class SelectableContainer : ICommand
{
    private List<Selectable> _selectables = new List<Selectable>();
    public List<Selectable> Selectables => _selectables;

    public SelectableContainer()
    {

    }

    public SelectableContainer(string[] parametorData)
    {

    }

    public async UniTask RunCommand(CancellationToken token = default)
    {
        // 選択肢の表示。
        foreach (var selectable in _selectables)
        {
            selectable.Show();
            selectable.OnSelected += OnSelected;
        }
        // ユーザーの選択を待機。
        var select = await OnSelected();
        // 表示されている選択肢を破棄する。
        foreach (var selectable in _selectables)
        {
            selectable.OnSelected -= OnSelected;
            selectable.Hide();
        }
        // 選択されたコマンドの実行。
        await select.RunCommand(token);
    }

    private Selectable _selectItem = null;

    private void OnSelected(Selectable select)
    {
        _selectItem = select;
    }

    private async UniTask<Selectable> OnSelected()
    {
        await UniTask.WaitUntil(() => _selectItem != null);
        var select = _selectItem;
        _selectItem = null;
        return select;
    }
}
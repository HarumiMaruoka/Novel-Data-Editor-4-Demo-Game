// 日本語対応
using Cysharp.Threading.Tasks;
using System.Threading;

public class FadeScreen : ICommand
{
    private float _targetValue;
    private float _duration;

    public FadeScreen(string[] commandArgs)
    {
        _targetValue = float.Parse(commandArgs[0]);
        _duration = float.Parse(commandArgs[1]);
    }

    public async UniTask RunCommand(CancellationToken token = default)
    {
        var screenImage = NovelData.Current.ScreenImage;
        var from = screenImage.color;
        var to = screenImage.color;
        to.a = _targetValue;
        var duration = _duration;
        await screenImage.FadeAsync(from, to, duration);
    }
}
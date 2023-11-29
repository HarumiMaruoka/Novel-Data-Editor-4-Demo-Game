// 日本語対応
using Cysharp.Threading.Tasks;
using System.Threading;

public class ActorAction : ICommand
{
    public ActorAction(string[] parametorData)
    {
        throw new System.NotImplementedException();
    }

    public UniTask RunCommand(CancellationToken token = default)
    {
        throw new System.NotImplementedException();
    }

    // TODO: Actorに必要そうなアクション。 フェード、移動
}
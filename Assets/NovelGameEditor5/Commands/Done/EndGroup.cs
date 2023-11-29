// 日本語対応
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class EndGroup : ICommand
{
    public EndGroup()
    {

    }

    public EndGroup(string[] parametorData)
    {

    }

#pragma warning disable 1998
    public async UniTask RunCommand(CancellationToken token = default)
    {

    }
#pragma warning restore 1998
}
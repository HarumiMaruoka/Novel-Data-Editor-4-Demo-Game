// 日本語対応
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class BackgroundAction : ICommand
{
    public BackgroundAction(string[] parametorData)
    {
        throw new System.NotImplementedException();
    }
    public UniTask RunCommand(CancellationToken token = default)
    {
        throw new System.NotImplementedException();
    }

    // Fadeなど背景に関するアクションの実装。
}
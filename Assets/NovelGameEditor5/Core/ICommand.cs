// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public interface ICommand
{
    UniTask RunCommand(CancellationToken token = default); // コマンドを実行する待機可能。
}
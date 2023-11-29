// 日本語対応
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class CommandRunner : MonoBehaviour
{
    public void Start()
    {
        var commands = CommandLoader.LoadSheet(
            @"
            [BeginGroup]
            [FadeScreen,0,1]
            [PrintText,Hello,0.1]
            [EndGroup]
            [PrintText,Hello1,0.1]
            [PrintText,Hello2,0.1]
            [PrintText,Hello3,0.1]
            [PrintText,Hello4,0.1]
            [BeginSelectGroup]
            [Selectable,Selectable 1]
            [LoadScene,SampleScene1]
            [Selectable,Selectable2]
            [LoadScene,SampleScene2]
            [Selectable,Selectable3]
            [LoadScene,SampleScene3]
            [EndSelectGroup]
            ");

        var executable = MakeExecutable(commands);
        RunCommands(executable);
    }

    public async void RunCommands(List<List<ICommand>> executable)
    {
        var token = this.GetCancellationTokenOnDestroy();
        for (int i = 0; i < executable.Count; i++)
        {
            UniTask[] tasks = new UniTask[executable[i].Count];
            for (var j = 0; j < executable[i].Count; j++)
            {
                tasks[j] = executable[i][j].RunCommand(token);
            }
            await UniTask.WhenAll(tasks);
            if (token.IsCancellationRequested) return;
        }
    }

    private List<List<ICommand>> MakeExecutable(ICommand[] commands)
    {
        bool groupFlag = false;
        List<List<ICommand>> result = new List<List<ICommand>>();
        List<ICommand> currentCollection = null;
        SelectableContainer currentSelectableContainer = null;
        Selectable lastSelectable = null;

        currentCollection = new List<ICommand>();
        result.Add(currentCollection);

        for (int i = 0; i < commands.Length; i++)
        {
            var command = commands[i];

            if (command is BeginGroup)
            {
                groupFlag = true;
                command = null;
            }
            else if (command is EndGroup)
            {
                groupFlag = false;
                command = null;
            }
            else if (command is BeginSelectGroup)
            {
                currentSelectableContainer = new SelectableContainer();
                command = currentSelectableContainer;
            }
            else if (command is Selectable)
            {
                lastSelectable = command as Selectable;
                command = null;
                currentSelectableContainer.Selectables.Add(lastSelectable);
            }
            else if (command is EndSelectGroup)
            {
                lastSelectable = null;
                currentSelectableContainer = null;
            }

            if (currentSelectableContainer != null && lastSelectable != null)
            {
                currentCollection = lastSelectable.Commands;
            }
            else if (!groupFlag)
            {
                if (currentCollection.Count != 0)
                {
                    currentCollection = new List<ICommand>();
                    result.Add(currentCollection);
                }
            }

            if (command != null) currentCollection.Add(command);
        }

        return result;
    }
}
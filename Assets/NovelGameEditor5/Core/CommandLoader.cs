// 日本語対応
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;

public static class CommandLoader
{
    public static ICommand ToCommand(this string[] commandData)
    {
        string commandName = commandData[0];
        string[] commandArgs = commandData[1..];

        var types = TypeCache.GetTypesDerivedFrom<ICommand>();

        foreach (var type in types)
        {
            if (commandName == type.ToString())
            {
                var constructor = type.GetConstructor(new[] { typeof(string[]) });
                if (constructor != null)
                {
                    return constructor.Invoke(new object[] { commandArgs }) as ICommand;
                }
            }
        }

        throw new ArgumentException(nameof(commandName));
    }

    public static ICommand[] LoadSheet(string commandSheet)
    {
        var commandStrData = ParseCommands(commandSheet);
        var commandCount = commandStrData.Count;
        ICommand[] commands = new ICommand[commandCount];

        for (int i = 0; i < commandCount; i++)
        {
            commands[i] = commandStrData[i].ToCommand();
        }

        return commands;
    }

    public static List<string[]> ParseCommands(string input)
    {
        List<string[]> commands = new List<string[]>();

        string pattern = @"\[(.*?)\]";
        MatchCollection matches = Regex.Matches(input, pattern);

        foreach (Match match in matches)
        {
            string commandText = match.Groups[1].Value;
            string[] commandArgs = commandText.Split(',');

            commands.Add(commandArgs);
        }

        return commands;
    }
}
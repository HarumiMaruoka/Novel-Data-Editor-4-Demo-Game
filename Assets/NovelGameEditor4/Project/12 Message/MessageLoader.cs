// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

public class MessageLoader
{
    private static readonly string[] SheetIDs = new string[]
    {
        "1p5A1cniJfCtn-YBf-OjV_iXj3wpz-MZEzaFHaZrhsd0",
    };

    // sheetIDの指定方法: https://qiita.com/simanezumi1989/items/32436230dadf7a123de8#:~:text=%E7%94%BB%E5%83%8F1-,SHEET_ID%E3%81%AF%E4%BB%A5%E4%B8%8B%E3%81%AE,-https%3A//docs.google
    // sheetNameの指定方法: https://qiita.com/simanezumi1989/items/32436230dadf7a123de8#:~:text=%E7%94%BB%E5%83%8F2-,SHEET_NAME%E3%81%AF%E4%BB%A5%E4%B8%8B%E3%81%AE%E6%96%87%E5%AD%97%E5%88%97,-%E5%BF%9C%E7%94%A8%EF%BC%88%E3%81%8A%E5%8C%96%E7%B2%A7%E7%9B%B4%E3%81%97

    private static Dictionary<RequestData, Message[]> _resultCache = new Dictionary<RequestData, Message[]>();

    public static async UniTask<Message[]> LoadMessagesAsync(RequestData requestData, CancellationToken token)
    {
        if (requestData.SheetID < 0 || requestData.SheetID >= SheetIDs.Length)
            throw new ArgumentOutOfRangeException(nameof(requestData.SheetID));

        if (_resultCache.TryGetValue(requestData, out var value))
            return value;

        var sheetData = await GoogleSheetsLoader.LoadGoogleSheetsAsync(SheetIDs[requestData.SheetID], requestData.SheetName, requestData.IgnoreRows, token);
        var message = SheetDataToMessage(sheetData);
        _resultCache.Add(requestData, message);
        return message;
    }

    private static Message[] SheetDataToMessage(List<string[]> result)
    {
        Message[] messages = new Message[result.Count];
        for (int i = 0; i < result.Count; i++)
        {
            var personName = result[i][0];
            var text = result[i][1];

            messages[i] = new Message(personName, text);
        }
        return messages;
    }

    public struct RequestData
    {
        public RequestData(int sheetID, string sheetName, int ignoreRows = 0)
        {
            this.SheetID = sheetID;
            this.SheetName = sheetName;
            this.IgnoreRows = ignoreRows;
        }

        public int SheetID { get; }
        public string SheetName { get; }
        public int IgnoreRows { get; }
    }
}
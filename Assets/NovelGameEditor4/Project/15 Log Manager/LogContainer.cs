// 日本語対応
using System;
using System.Collections;
using System.Collections.Generic;

public class LogContainer : IEnumerable<Message>
{
    private List<Message> _log = new List<Message>();

    public event Action<Message> OnAddedLog;
    public int Count => _log.Count;

    public void Initialize()
    {
        _log.Clear();
    }

    public void Add(Message message)
    {
        _log.Add(message);
        OnAddedLog?.Invoke(message);
    }

    #region Implementation of IEnumerable 
    public IEnumerator<Message> GetEnumerator()
    {
        return _log.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion

    #region Indexer
    public Message this[int index]
    {
        get
        {
            if (index < 0 || index >= _log.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return _log[index];
        }
        set
        {
            if (index < 0 || index >= _log.Count)
            {
                throw new IndexOutOfRangeException();
            }
            _log[index] = value;
        }
    }
    #endregion
}
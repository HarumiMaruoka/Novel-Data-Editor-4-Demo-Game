// 日本語対応
using UnityEngine;

public class LogWindow : MonoBehaviour
{
    [SerializeField]
    private LogView _logViewPrefab;
    [SerializeField]
    private Transform _logParent;

    private LogContainer LogContainer => LogManager.Instance.Container;

    private void Start()
    {
        for (int i = 0; i < LogContainer.Count; i++)
        {
            CreateLog(LogContainer[i]);
        }
        LogContainer.OnAddedLog += AddLogView;
    }

    private void OnDestroy()
    {
        LogContainer.OnAddedLog -= AddLogView;
    }

    public void AddLogView(Message message)
    {
        CreateLog(message);
    }

    public LogView CreateLog(Message message)
    {
        var instance = Instantiate(_logViewPrefab, _logParent);
        instance.PersonName.text = message.PersonName;
        instance.Text.text = message.Text;
        return instance;
    }
}
// 日本語対応
using UnityEngine;

public class LogManager
{
    private static LogManager _instance = null;
    public static LogManager Instance => _instance ??= new LogManager();

    private LogManager() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Instance._container.Initialize();
    }

    private LogContainer _container = new LogContainer();
    public LogContainer Container => _container;
}
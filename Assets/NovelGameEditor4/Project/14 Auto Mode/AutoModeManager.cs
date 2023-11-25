// 日本語対応
using System;
using UnityEngine;

public class AutoModeManager
{
    private static AutoModeManager _instance = null;
    public static AutoModeManager Instance => _instance ??= new AutoModeManager();

    private AutoModeManager() { }

    private bool _isAutoMode = false;

    public bool IsAutoMode => _isAutoMode;

    public event Action OnAutoModeStarted;
    public event Action OnAutoModeStoped;

    public void StartAutoMode()
    {
        if (!_isAutoMode)
        {
            OnAutoModeStarted?.Invoke();
            _isAutoMode = true;
        }
        else
        {
            Debug.Log("Auto mode is already active.");
        }
    }

    public void StopAutoMode()
    {
        if (_isAutoMode)
        {
            OnAutoModeStoped?.Invoke();
            _isAutoMode = false;
        }
        else
        {
            Debug.Log("Auto mode is already stopped.");
        }
    }
}
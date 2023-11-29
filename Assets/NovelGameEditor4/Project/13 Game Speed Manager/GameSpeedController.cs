// 日本語対応
using System;

public class GameSpeedController
{
    private static GameSpeedController _instance = null;
    public static GameSpeedController Instance => _instance ??= new GameSpeedController();

    private int _pauseCount = 0;
    public int PauseCount => _pauseCount;

    private float _cachedGameSpeed = 1f;
    public float CachedGameSpeed => _cachedGameSpeed;

    public bool IsPaused => _pauseCount > 0;
    public float GameSpeed => IsPaused ? 0f : _cachedGameSpeed;

    private Action _onPaused;
    private Action _onResumed;

    public event Action OnPaused
    {
        add
        {
            if (IsPaused) value?.Invoke();
            _onPaused += value;
        }
        remove
        {
            _onPaused -= value;
        }
    }
    public event Action OnResumed
    {
        add
        {
            _onResumed += value;
        }
        remove
        {
            _onResumed -= value;
        }
    }

    public void Pause()
    {
        _pauseCount++;
        if (_pauseCount == 1)
        {
            _onPaused?.Invoke();
        }
    }

    public void Resume()
    {
        _pauseCount--;
        if (_pauseCount == 0)
        {
            _onResumed?.Invoke();
        }
    }
}
// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class AutoModeController : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _stopButton;

    private void Start()
    {
        _startButton.onClick.AddListener(StartAutoMode);
        _stopButton.onClick.AddListener(StopAutoMode);
    }

    private void StartAutoMode()
    {
        AutoModeManager.Instance.StartAutoMode();
    }

    private void StopAutoMode()
    {
        AutoModeManager.Instance.StopAutoMode();
    }
}
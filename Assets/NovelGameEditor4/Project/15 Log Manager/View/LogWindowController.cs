// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class LogWindowController : MonoBehaviour
{
    [SerializeField]
    private LogWindow _logWindow;
    [SerializeField]
    private bool _initialEnable = false;

    [SerializeField]
    private Button _openButton;
    [SerializeField]
    private Button _closeButton;

    private GameSpeedController GameSpeedController => GameSpeedController.Instance;

    private void Start()
    {
        _logWindow.gameObject.SetActive(_initialEnable);
        _openButton.onClick.AddListener(OpenWindow);
        _closeButton.onClick.AddListener(CloseWindow);
    }

    private void OpenWindow()
    {
        if (_logWindow.gameObject.activeSelf)
        {
            Debug.Log("LogWindow is already open.");
            return;
        }
        GameSpeedController.Pause();
        _logWindow.gameObject.SetActive(true);
    }

    private void CloseWindow()
    {
        if (!_logWindow.gameObject.activeSelf)
        {
            Debug.Log("LogWindow is already closed.");
            return;
        }
        GameSpeedController.Resume();
        _logWindow.gameObject.SetActive(false);
    }
}
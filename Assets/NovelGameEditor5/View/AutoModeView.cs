// 日本語対応
using UnityEngine;

public class AutoModeView : MonoBehaviour
{
    [SerializeField]
    private GameObject _viewObject;

    private void Start()
    {
        _viewObject.SetActive(AutoModeManager.Instance.IsAutoMode);
        AutoModeManager.Instance.OnAutoModeStarted += AutoModeStarted;
        AutoModeManager.Instance.OnAutoModeStoped += AutoModeStoped;
    }

    private void OnDestroy()
    {
        AutoModeManager.Instance.OnAutoModeStarted -= AutoModeStarted;
        AutoModeManager.Instance.OnAutoModeStoped -= AutoModeStoped;
    }

    private void AutoModeStarted()
    {
        _viewObject.SetActive(true);
    }

    private void AutoModeStoped()
    {
        _viewObject.SetActive(false);
    }
}
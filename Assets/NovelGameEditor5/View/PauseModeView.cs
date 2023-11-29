// 日本語対応
using UnityEngine;

public class PauseModeView : MonoBehaviour
{
    [SerializeField]
    private GameObject _viewObject;

    private void Start()
    {
        _viewObject.SetActive(GameSpeedController.Instance.IsPaused);
        GameSpeedController.Instance.OnPaused += Paused;
        GameSpeedController.Instance.OnResumed += Resumed;
    }

    private void OnDestroy()
    {
        GameSpeedController.Instance.OnPaused -= Paused;
        GameSpeedController.Instance.OnResumed -= Resumed;
    }

    private void Paused()
    {
        _viewObject.SetActive(true);
    }

    private void Resumed()
    {
        _viewObject.SetActive(false);
    }
}
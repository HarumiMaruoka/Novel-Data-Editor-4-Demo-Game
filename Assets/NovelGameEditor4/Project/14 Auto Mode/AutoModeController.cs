// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class AutoModeController : MonoBehaviour
{
    [SerializeField]
    private Button _autoModeButton;

    private void Start()
    {
        _autoModeButton.onClick.AddListener(SwitchAutoMode);
    }

    private void SwitchAutoMode()
    {
        // オートモードボタンが押されたとき
        // オートモードであればオートモードを解除する。
        // そうでなければ、オートモードにする。
        if (AutoModeManager.Instance.IsAutoMode) AutoModeManager.Instance.StopAutoMode();
        else AutoModeManager.Instance.StartAutoMode();
    }
}
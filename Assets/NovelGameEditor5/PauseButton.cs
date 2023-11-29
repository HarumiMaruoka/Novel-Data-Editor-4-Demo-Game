// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    bool _pressed = false;

    private void Start()
    {
        _button.onClick.AddListener(Pressed);
    }

    private void Pressed()
    {
        if (!_pressed)
        {
            _pressed = true;
            GameSpeedController.Instance.Pause();
        }
        else
        {
            _pressed = false;
            GameSpeedController.Instance.Resume();
        }
    }
}
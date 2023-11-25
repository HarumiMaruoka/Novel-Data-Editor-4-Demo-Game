// 日本語対応
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneLoader
{
    [SerializeField]
    private Fade _fade;

    private bool _playing = false;
    public async void LoadScene(string sceneName)
    {
        if (_playing) return;
        _playing = true;

        GameSpeedController.Instance.Pause();
        await _fade.FadeIn();
        GameSpeedController.Instance.Resume();

        SceneManager.LoadScene(sceneName);
    }
}
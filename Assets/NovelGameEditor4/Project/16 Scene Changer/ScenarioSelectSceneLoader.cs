// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class ScenarioSelectSceneLoader : MonoBehaviour
{
    [SerializeField]
    private string _scenarioSelectSceneName;
    [SerializeField]
    private SceneLoader _changer;
    [SerializeField]
    private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(() => _changer.LoadScene(_scenarioSelectSceneName));
    }
}
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button closeSettingsButton;

    [Header("General UI")]
    [SerializeField] private CanvasGroup mainMenuCanvas;
    [SerializeField] private CanvasGroup settingsCanvas;

    private void Awake() {
        InitializeButtonsBehaviour();
    }

    private void Start() {
        mainMenuCanvas.Toggle(true);
        settingsCanvas.Toggle(false);
    }

    private void InitializeButtonsBehaviour() {
        startGameButton.onClick.AddListener(OnStartButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        exitButton.onClick.AddListener(OnExitButtonPressed);
        closeSettingsButton.onClick.AddListener(OnCloseSettingsButton);
    }

    private void OnStartButtonPressed() {
        ServiceLocator.Instance.GetService<SceneLoader>().LoadScene(Constants.SCENE_GAME, 1.5f);
    }

    private void OnSettingsButtonPressed() {
        mainMenuCanvas.Toggle(false);
        settingsCanvas.Toggle(true);
    }

    private void OnCloseSettingsButton() {
        mainMenuCanvas.Toggle(true);
        settingsCanvas.Toggle(false);
    }

    private void OnExitButtonPressed() {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
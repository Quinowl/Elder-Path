using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

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
    }

    private void OnStartButtonPressed() {

    }

    private void OnSettingsButtonPressed() {
        mainMenuCanvas.Toggle(false);
        settingsCanvas.Toggle(true);
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
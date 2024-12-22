using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    [Header("General UI")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private CanvasGroup mainMenuCanvas;
    [SerializeField] private CanvasGroup settingsCanvas;
    [SerializeField] private SettingMenu settingMenu;

    private void Awake() {
        InitializeButtonsBehaviour();
        settingMenu.InitializeSettings();
    }

    private void Start() {
        ServiceLocator.Instance.GetService<MusicPlayer>().PlayMenuTheme();
        mainMenuCanvas.Toggle(true);
        settingsCanvas.Toggle(false);
        eventSystem.SetSelectedGameObject(startGameButton.gameObject);
    }

    private void InitializeButtonsBehaviour() {
        startGameButton.onClick.AddListener(OnStartButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        exitButton.onClick.AddListener(OnExitButtonPressed);
        settingMenu.SetListenersToCloseButton(OnCloseSettingsButton);
    }

    private void OnStartButtonPressed() {
        ServiceLocator.Instance.GetService<MusicPlayer>().PlayGameTheme();
        ServiceLocator.Instance.GetService<SceneLoader>().LoadScene(Constants.Scenes.GAME, 1.5f);
    }

    private void OnSettingsButtonPressed() {
        mainMenuCanvas.Toggle(false);
        settingsCanvas.Toggle(true);
        eventSystem.SetSelectedGameObject(settingMenu.FirstSelectedObject);
    }

    private void OnCloseSettingsButton() {
        mainMenuCanvas.Toggle(true);
        settingsCanvas.Toggle(false);
        eventSystem.SetSelectedGameObject(startGameButton.gameObject);
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
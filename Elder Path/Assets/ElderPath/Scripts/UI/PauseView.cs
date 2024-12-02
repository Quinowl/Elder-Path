using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour {

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button menuButton;

    private GameMenuMediator gameMenuMediator;

    public void Configure(GameMenuMediator mediator) => gameMenuMediator = mediator;

    private void Start() {
        InitializeButtons();
    }

    private void InitializeButtons() {
        resumeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        menuButton.onClick.RemoveAllListeners();

        resumeButton.onClick.AddListener(OnResumeButton);
        restartButton.onClick.AddListener(OnRestartButton);
        settingsButton.onClick.AddListener(OnSettingsButton);
        menuButton.onClick.AddListener(OnMenuButton);
    }

    public void Show() => canvasGroup.Toggle(true);

    public void Hide() => canvasGroup.Toggle(false);

    private void OnResumeButton() => gameMenuMediator.OnResumeButtonPressed();

    private void OnRestartButton() => gameMenuMediator.OnRestartButtonPressed();

    private void OnSettingsButton() => gameMenuMediator.OnSettingsButtonPressed();

    private void OnMenuButton() => gameMenuMediator.OnBackToMenuButtonPressed();
}
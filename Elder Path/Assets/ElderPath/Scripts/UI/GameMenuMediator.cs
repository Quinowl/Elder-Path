using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameMenuMediator : MonoBehaviour {

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private PauseView pauseView;
    [SerializeField] private EndGameView endGameView;

    private bool isPaused;

    private void OnEnable() {
        EPGameManager.OnEndGame += OnEndGame;
    }

    private void Awake() {
        isPaused = false;
        pauseView.Configure(this);
        endGameView.Configure(this);
    }

    private void Start() {
        HideAllMenus();
    }

    private void OnDisable() {
        EPGameManager.OnEndGame -= OnEndGame;
    }

    public void OnPauseInput(InputAction.CallbackContext context) {
        if (context.started) TogglePause();
    }

    public void OnResumeButtonPressed() => TogglePause();

    public void OnSettingsButtonPressed() {

    }

    public void OnRestartButtonPressed() {
        TogglePause();
        ServiceLocator.Instance.GetService<EPLevelManager>().RestartLevelWithLoadingScreen();
    }

    public void OnBackToMenuButtonPressed() {
        if (isPaused) TogglePause();
        ServiceLocator.Instance.GetService<SceneLoader>().LoadScene(Constants.Scenes.MAIN_MENU);
    }

    public void OnUpdateSelectedObjectInEventSystem(GameObject obj) => eventSystem.SetSelectedGameObject(obj);

    private void TogglePause() {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        EPInputManager.Instance.SetEnableInput(!isPaused);
        if (isPaused) pauseView.Show();
        else pauseView.Hide();
    }

    private void HideAllMenus() {
        pauseView.Hide();
        endGameView.Hide();
    }

    private void OnEndGame() {
        endGameView.Show();
    }
}
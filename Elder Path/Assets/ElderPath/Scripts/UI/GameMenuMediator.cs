using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameMenuMediator : MonoBehaviour {

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private PauseView pauseView;
    [SerializeField] private EndGameView endGameView;
    [SerializeField] private GameView gameView;

    private bool isPaused;

    private void OnEnable() {
        EPGameManager.OnEndGame += OnEndGame;
    }

    private void Awake() {
        isPaused = false;
        pauseView.Configure(this);
        endGameView.Configure(this);
        gameView.Configure(this);
    }

    private void Start() {
        HideAllMenus();
        gameView.Show();
    }

    private void OnDisable() {
        EPGameManager.OnEndGame -= OnEndGame;
    }

    public void OnPauseInput(InputAction.CallbackContext context) {
        if (context.started) TogglePause();
    }

    public void OnResumeButtonPressed() => TogglePause();

    public void OnSettingsButtonPressed() {
        //TODO: Ajustar el menú de ajustes
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

    public void UpdateText(TMP_Text text, string message) => text.text = message;

    private void TogglePause() {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        EPInputManager.Instance.SetEnableInput(!isPaused);
        if (isPaused) {
            pauseView.Show();
            gameView.Hide();
        }
        else {
            pauseView.Hide();
            gameView.Show();
        }
    }

    private void HideAllMenus() {
        pauseView.Hide();
        endGameView.Hide();
        gameView.Hide();
    }

    private void OnEndGame() {
        endGameView.Show();
        gameView.Hide();
    }
}
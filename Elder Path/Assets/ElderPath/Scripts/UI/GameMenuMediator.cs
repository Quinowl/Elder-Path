using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuMediator : MonoBehaviour {

    [SerializeField] private PauseView pauseView;

    private bool isPaused;

    private void Awake() {

        isPaused = false;
        pauseView.Configure(this);
    }

    private void Start() {
        HideAllMenus();
    }

    public void OnPauseInput(InputAction.CallbackContext context) {
        if (context.started) TogglePause();
    }

    public void OnResumeButtonPressed() => TogglePause();

    public void OnSettingsButtonPressed() {

    }

    public void OnRestartButtonPressed() {
        HideAllMenus();
        // Resume y que recargue la escena con el nivel
    }

    public void OnBackToMenuButtonPressed() {
        TogglePause();
        ServiceLocator.Instance.GetService<SceneLoader>().LoadScene(Constants.SCENE_MAIN_MENU);
    }

    private void TogglePause() {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        EPInputManager.Instance.EnableInput(!isPaused);
        if (isPaused) pauseView.Show();
        else pauseView.Hide();
    }

    private void HideAllMenus() {
        pauseView.Hide();
    }
}
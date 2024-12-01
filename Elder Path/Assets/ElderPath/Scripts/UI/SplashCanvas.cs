using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SplashCanvas : MonoBehaviour {

    private bool hasStarted;

    public void OnAnyKeyPressed(InputAction.CallbackContext context) {
        if (!hasStarted && context.started) GoToMainMenu();
    }

    private void GoToMainMenu() {
        hasStarted = true;
        //TODO: Add loading screen, etc
        SceneManager.LoadScene(Constants.SCENE_MAIN_MENU);
    }
}
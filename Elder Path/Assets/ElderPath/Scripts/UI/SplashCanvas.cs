using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SplashCanvas : MonoBehaviour {

    [SerializeField] private SequenceAnimator sequenceAnimator;

    private bool hasStarted;

    private void Start() {
        sequenceAnimator.StartSequence();
    }

    public void OnAnyKeyPressed(InputAction.CallbackContext context) {
        if (!hasStarted && context.started) GoToMainMenu();
    }

    private void GoToMainMenu() {
        hasStarted = true;
        sequenceAnimator.StopSequence();
        SceneManager.LoadScene(Constants.SCENE_MAIN_MENU);
        ServiceLocator.Instance.GetService<SceneLoader>().LoadScene(Constants.SCENE_MAIN_MENU, 1f);
    }
}
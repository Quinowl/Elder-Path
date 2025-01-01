using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SplashCanvas : MonoBehaviour
{

    [SerializeField] private SequenceAnimator sequenceAnimator;

    private bool hasStarted;

    private void Start()
    {
        sequenceAnimator.StartSequence();
    }

    public void OnAnyKeyPressed(InputAction.CallbackContext context)
    {
        if (!hasStarted && context.started) GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        hasStarted = true;
        sequenceAnimator.StopSequence();
        ServiceLocator.Instance.GetService<SceneLoader>().LoadScene(Constants.Scenes.MAIN_MENU, 1f);
    }
}
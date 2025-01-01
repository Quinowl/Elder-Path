using UnityEngine;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField] private SequenceAnimator loadingTextAnimator;
    [SerializeField] private Animator imageAnimator;

    public void StartLoading()
    {
        gameObject.SetActive(true);
        loadingTextAnimator.StartSequence();
        imageAnimator.Play("loading-screen-running");
    }

    public void StopLoading()
    {
        loadingTextAnimator.StopSequence();
        imageAnimator.Play("loading-screen-wait");
        gameObject.SetActive(false);
    }
}
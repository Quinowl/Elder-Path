using UnityEngine;

public class LoadingScreen : MonoBehaviour {

    [SerializeField] private SequenceAnimator loadingTextAnimator;
    [SerializeField] private Animator imageAnimator;

    public void StartLoading() {
        loadingTextAnimator.StartSequence();
        imageAnimator.Play("loading-screen-running");
    }

    public void StopLoading() {
        loadingTextAnimator.StopSequence();
        imageAnimator.Play("loading-screen-wait");
    }
}
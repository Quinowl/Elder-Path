using System.Collections;
using UnityEngine;

public class SequenceAnimator : MonoBehaviour {

    [SerializeField] private Animator[] animators;
    [SerializeField] private float waitBetween = 0.15f;
    [SerializeField] private float waitEnd = 0.1f;

    private Coroutine sequenceCoroutine;

    public void StartSequence() {
        if (sequenceCoroutine == null) sequenceCoroutine = StartCoroutine(StartAnimation());
    }

    public void StopSequence() {
        if (sequenceCoroutine != null) {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
    }

    private IEnumerator StartAnimation() {
        while (true) {
            foreach (Animator animator in animators) {
                animator.Play("loading-screen-animated-text");
                yield return new WaitForSeconds(waitBetween);
            }
            yield return new WaitForSeconds(waitEnd);
        }
    }
}
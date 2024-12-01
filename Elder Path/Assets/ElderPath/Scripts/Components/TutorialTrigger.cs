using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    [SerializeField] private GameObject bubble;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Constants.TAG_PLAYER)) {

        }
    }
}
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private Animator animator;

    private void Awake() {
        if (!animator) animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out PlayerController player)) {
            animator.SetTrigger(Constants.COIN_ANIMATION_PICK_UP);
            Destroy(gameObject, .75f);
        }
    }
}
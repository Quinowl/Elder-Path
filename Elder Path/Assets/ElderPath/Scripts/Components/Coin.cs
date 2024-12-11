using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private Animator animator;

    private void Awake() {
        if (!animator) animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out PlayerController player)) {
            //TODO: Add coin to player
            animator.Play(Constants.MiscAnimations.COIN_PICK_UP);
            Destroy(gameObject, .75f);
        }
    }
}
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider2d;

    private void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!collider2d) collider2d = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            //TODO: Add coin to player
            animator.Play(Constants.MiscAnimations.COIN_PICK_UP);
            ServiceLocator.Instance.GetService<EPSoundsManager>().PlaySFX(Constants.SFXIDs.MISC_PICK_UP_COIN, transform);
            collider2d.enabled = false;
            Destroy(gameObject, .75f);
        }
    }
}
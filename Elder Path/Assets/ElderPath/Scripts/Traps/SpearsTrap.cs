using UnityEngine;

public class SpearsTrap : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerDetecter playerDetecter;
    [SerializeField] private ObstacleDamageDealer damageDealer;
    [SerializeField] private Collider2D staticHitboxCollider;
    [SerializeField] private Collider2D fallingHitboxCollider;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite fallingSprite;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private Animator collisionAnimator;

    private void Awake()
    {
        CheckReferences();
        spriteRenderer.sprite = idleSprite;
        rigidbody2d.simulated = false;
        staticHitboxCollider.enabled = false;
        fallingHitboxCollider.enabled = false;
        collisionAnimator.gameObject.SetActive(false);
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Start()
    {
        playerDetecter.OnPlayerDetected += FallSpears;
        damageDealer.OnCollision += OnCollision;
    }

    private void OnDestroy()
    {
        playerDetecter.OnPlayerDetected -= FallSpears;
        damageDealer.OnCollision -= OnCollision;
    }

    private void CheckReferences()
    {
        if (!idleSprite || !fallingSprite) Debug.LogError("Idle sprite or falling sprite is not configured.");
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (!playerDetecter) playerDetecter = GetComponentInChildren<PlayerDetecter>();
        if (!rigidbody2d) rigidbody2d = GetComponentInChildren<Rigidbody2D>();
    }

    private void FallSpears()
    {
        spriteRenderer.sprite = fallingSprite;
        rigidbody2d.simulated = true;
        fallingHitboxCollider.enabled = true;
    }

    private void OnCollision(Vector3 collisionPoint)
    {
        fallingHitboxCollider.enabled = false;
        staticHitboxCollider.enabled = true;
        Vector2 effectPoint = new Vector2(fallingHitboxCollider.bounds.center.x, collisionPoint.y);
        collisionAnimator.gameObject.transform.position = effectPoint;
        collisionAnimator.gameObject.SetActive(true);
        collisionAnimator.Play("collision-effect");
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
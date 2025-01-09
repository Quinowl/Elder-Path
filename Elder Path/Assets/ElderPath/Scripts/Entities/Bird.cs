using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sprite;
    [Header("Configuration")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float arrivedDistance = 0.1f;

    private Vector2 targetPosition = Vector2.zero;

    private void Awake()
    {
        if (!sprite) sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleMovement();
        CheckArrival();
    }

    public void SetFinalPoint(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        UpdateFlip();
    }

    private void UpdateFlip() => sprite.flipX = transform.position.x <= targetPosition.x;

    private void HandleMovement()
    {
        if (targetPosition == Vector2.zero) return;
        Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void CheckArrival()
    {
        if (Vector2.Distance(transform.position, targetPosition) <= arrivedDistance)
            Destroy(gameObject);
    }
}
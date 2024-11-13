using UnityEngine;

public class CharacterCollisions : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform[] groundCheckPositions;
    [SerializeField] private Transform[] ceilCheckPositions;
    [SerializeField] private Transform[] frontCheckPositions;

    [Header("Configuration")]
    [SerializeField] private LayerMask groundAndCeilLayer;
    [SerializeField] private float groundRayLength;
    [SerializeField] private float ceilRayLength;
    [SerializeField] private LayerMask frontObstaclesLayer;
    [SerializeField] private float frontRayLength;

    public bool IsGrounded { get; private set; }
    public bool IsCeiled { get; private set; }
    public bool HasSomethingInFront { get; private set; }
    public RaycastHit2D GroundHit => groundHit;

    private RaycastHit2D groundHit;

    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        foreach (Transform point in groundCheckPositions) {
            Gizmos.DrawRay(point.position, Vector2.down * groundRayLength);
        }

        Gizmos.color = Color.blue;
        foreach (Transform point in ceilCheckPositions) {
            Gizmos.DrawRay(point.position, Vector2.up * ceilRayLength);
        }

        Gizmos.color = Color.cyan;
        foreach (Transform point in frontCheckPositions) {
            Gizmos.DrawRay(point.position, (transform.localScale.x == 1 ? Vector2.right : Vector2.left) * frontRayLength);
        }
    }

    private void Update() {
        PerformCollisionChecks();
    }

    private void PerformCollisionChecks() {
        IsGrounded = CheckForCollisions(groundCheckPositions, Vector2.down, groundRayLength, groundAndCeilLayer, out groundHit);
        IsCeiled = CheckForCollisions(ceilCheckPositions, Vector2.up, ceilRayLength, groundAndCeilLayer, out _);
        HasSomethingInFront = CheckForCollisions(frontCheckPositions, transform.localScale.x == 1 ? Vector2.right : Vector2.left, frontRayLength, frontObstaclesLayer, out _);
    }

    private bool CheckForCollisions(Transform[] points, Vector2 direction, float rayLength, LayerMask layer, out RaycastHit2D hit) {
        hit = new();
        foreach (Transform point in points) {
            hit = Physics2D.Raycast(point.position, direction, rayLength, layer);
            if (hit.collider != null) return true;
        }
        return false;
    }
}
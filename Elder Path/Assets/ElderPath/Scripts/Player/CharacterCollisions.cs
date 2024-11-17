using UnityEngine;

public class CharacterCollisions : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform[] groundCheckPositions;
    [SerializeField] private Transform[] ceilCheckPositions;
    [SerializeField] private Transform[] frontCheckPositions;

    [Header("Configuration")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRayLength;
    [SerializeField] private LayerMask ceilLayer;
    [SerializeField] private float ceilRayLength;
    [SerializeField] private LayerMask frontObstaclesLayer;
    [SerializeField] private float frontRayLength;

    public bool IsGrounded { get; private set; }
    public bool IsCeiled { get; private set; }
    public bool HasSomethingInFront { get; private set; }
    public RaycastHit2D GroundHit => groundHit;

    private RaycastHit2D groundHit;

    private void OnDrawGizmos() {
        DrawGizmo(Color.red, groundCheckPositions, Vector2.down, groundRayLength);
        DrawGizmo(Color.blue, ceilCheckPositions, Vector2.up, ceilRayLength);
        DrawGizmo(Color.cyan, frontCheckPositions, transform.localScale.x == 1 ? Vector2.right : Vector2.left, frontRayLength);
    }

    private void Update() {
        PerformCollisionChecks();
    }

    private void DrawGizmo(Color gizmoColor, Transform[] points, Vector2 direction, float length) {
        Gizmos.color = gizmoColor;
        foreach (Transform point in points) {
            Gizmos.DrawRay(point.position, direction * length);
        }
    }

    private void PerformCollisionChecks() {
        IsGrounded = CheckForCollisions(groundCheckPositions, Vector2.down, groundRayLength, groundLayer, out groundHit);
        IsCeiled = CheckForCollisions(ceilCheckPositions, Vector2.up, ceilRayLength, ceilLayer, out _);
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
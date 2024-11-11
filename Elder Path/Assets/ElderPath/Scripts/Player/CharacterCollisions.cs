using UnityEngine;

public class CharacterCollisions : MonoBehaviour {

    [Header("References")]   
    [SerializeField] private Transform[] groundCheckPositions;
    [SerializeField] private Transform[] ceilCheckPositions;

    [Header("Configuration")]
    [SerializeField] private LayerMask groundAndCeilLayer;
    [SerializeField] private float groundRayLength;
    [SerializeField] private float ceilRayLength;

    public bool IsGrounded { get; private set; }
    public bool IsCeiled { get; private set; }
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
    }

    private void Update() {
        // GroundCheck();
        // CheckCeil();
        PerformCollisionChecks();
    }

    private void PerformCollisionChecks() {
        IsGrounded = CheckForCollisions(groundCheckPositions, Vector2.down, groundRayLength, out groundHit);
        IsCeiled = CheckForCollisions(ceilCheckPositions, Vector2.up, ceilRayLength, out _);
    }

    private bool CheckForCollisions(Transform[] points, Vector2 direction, float rayLength, out RaycastHit2D hit) {
        hit = new();
        foreach (Transform point in points) {
            hit = Physics2D.Raycast(point.position, direction, rayLength, groundAndCeilLayer);
            if (hit.collider != null) return true;
        }
        return false;
    }

    private void GroundCheck() {
        foreach (Transform point in groundCheckPositions) {
                groundHit = Physics2D.Raycast(point.position, Vector2.down ,groundRayLength, groundAndCeilLayer);
                if (GroundHit.collider) {
                    IsGrounded = true;
                    return;
                }
        IsGrounded = false;
        }
    }

    private void CheckCeil() {
        foreach (Transform point in ceilCheckPositions) {
            if (Physics2D.Raycast(point.position, Vector2.up ,ceilRayLength, groundAndCeilLayer)) {
                IsCeiled = true;
                return;
            }
        }
        IsCeiled = false;
    }

}
using UnityEngine;

public class CharacterCollisions : MonoBehaviour {

    [Header("References")]   
    [SerializeField] private Transform[] groundCheckPositions;

    [Header("Configuration")]
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded { get; private set; }
    public RaycastHit2D GroundHit { get; private set; }
    [field: SerializeField] public float GroundRayLength { get; private set; }

    private float initialGroundRayLength;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (Transform point in groundCheckPositions) {
            Gizmos.DrawRay(point.position, Vector2.down * GroundRayLength);
        }
    }

    private void Awake() {
        initialGroundRayLength = GroundRayLength;
    }

    private void Update() {
        GroundCheck();
    }

    private void GroundCheck() {
        foreach (Transform point in groundCheckPositions) {
                GroundHit = Physics2D.Raycast(point.position, Vector2.down ,GroundRayLength, groundLayer);
                if (GroundHit.collider) {
                    IsGrounded = true;
                    return;
                }
        IsGrounded = false;
        }
    }

    public void SetGroundRayLength(float newLength) {
        GroundRayLength = newLength;
    }

    public void RestoreGroundRayLength() => GroundRayLength = initialGroundRayLength;
}
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerConfiguration configuration;
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field: SerializeField] public Collider2D Collider2D { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Transform AttackPoint { get; private set; }
    [field: SerializeField] public CharacterCollisions Collisions { get; private set; }

    [Header("Gameplay")]
    [SerializeField] private AreaChecker frontalCheck;
    public bool IsGrounded => Collisions.IsGrounded;
    public bool IsCeiled => Collisions.IsCeiled;
    public bool IsFrontBlocked => frontalCheck.IsOverlapping();
    public bool CanMove { get; private set; }

    private bool groundedLastFrame;

    public void SetCanMove(bool canMove) => CanMove = canMove;

    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
        SetCanMove(true);
    }

    private void Update() {
        stateMachine.Step();
        CheckFlip();
        ApplyGravity();
    }

    private void FixedUpdate() {
        stateMachine.PhysicsStep();
    }

    private void LateUpdate() {
        stateMachine.LateStep();
    }

    private void ApplyGravity() {

        if (!IsGrounded) {
            Rigidbody2D.linearVelocityY += configuration.GravityForce * Time.deltaTime;
            if (Rigidbody2D.linearVelocityY < configuration.MaxFalligSpeed) Rigidbody2D.linearVelocityY = configuration.MaxFalligSpeed;
        }
        else if (Rigidbody2D.linearVelocityY < 0 && !groundedLastFrame) {
            Rigidbody2D.linearVelocityY = 0f;
            if (Collisions.GroundHit.distance > 0.025f) Rigidbody2D.position -= Vector2.up * 0.005f;
            if (Collisions.GroundHit.distance < 0.02f) Rigidbody2D.position += Vector2.up * (0.02f - Collisions.GroundHit.distance);
        }
        groundedLastFrame = IsGrounded;
    }

    private void CheckFlip() {
        if (Rigidbody2D.linearVelocityX == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = Rigidbody2D.linearVelocityX > 0 ? 1 : -1;
        transform.localScale = scale;
    }

    private void CheckCanMove() => SetCanMove(!IsFrontBlocked);
}
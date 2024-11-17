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

    public bool IsGrounded => Collisions.IsGrounded;
    public bool IsCeiled => Collisions.IsCeiled;
    public bool IsFrontBlocked => Collisions.HasSomethingInFront;

    public bool CanJumpCoyote => coyoteTimeCounter < configuration.CoyoteTime;

    private bool groundedLastFrame;
    private float coyoteTimeCounter;

    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
    }

    private void Update() {
        stateMachine.Step();
        CheckFlip();
        ApplyGravity();
        CheckCoyoteTime();
    }

    private void FixedUpdate() {
        stateMachine.PhysicsStep();
    }

    private void LateUpdate() {
        stateMachine.LateStep();
    }

    public void TryMove(float motion) {
        if (IsFrontBlocked) {
            Rigidbody2D.linearVelocityX = 0f;
            return;
        }
        Rigidbody2D.linearVelocityX = motion;
    }

    private void ApplyGravity() {
        if (!IsGrounded) {
            Rigidbody2D.linearVelocityY += configuration.GravityForce * Time.deltaTime;
            if (Rigidbody2D.linearVelocityY < configuration.MaxFalligSpeed) Rigidbody2D.linearVelocityY = configuration.MaxFalligSpeed;
        }
        else if (Rigidbody2D.linearVelocityY < 0 && !groundedLastFrame) {
            Rigidbody2D.linearVelocityY = 0f;
            //TODO: Fix these magic numbers
            if (Collisions.GroundHit.distance > 0.025f) Rigidbody2D.position -= Vector2.up * 0.005f;
            if (Collisions.GroundHit.distance < 0.02f) Rigidbody2D.position += Vector2.up * (0.02f - Collisions.GroundHit.distance);
        }
        groundedLastFrame = IsGrounded;
        Animator.SetBool(Constants.PLAYER_ANIMATOR_IS_GROUNDED, IsGrounded);
        Animator.SetFloat(Constants.PLAYER_ANIMATOR_Y_SPEED, Rigidbody2D.linearVelocityY);
    }

    private void CheckFlip() {
        if (EPInputManager.Instance.MoveInput == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = EPInputManager.Instance.MoveInput > 0 ? 1 : -1;
        transform.localScale = scale;
    }

    private void CheckCoyoteTime() {
        if (!IsGrounded) {
            coyoteTimeCounter += Time.deltaTime;
        }
        else {
            coyoteTimeCounter = 0f;
        }
    }
}
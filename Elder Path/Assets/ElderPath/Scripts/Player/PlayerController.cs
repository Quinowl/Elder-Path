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
    [field: SerializeField] public TrailRenderer TrailRenderer { get; private set; }
    [field: SerializeField] public ParticleSystem MovementParticles { get; private set; }
    public bool CanDash { get; private set; }
    public void SetCanDash(bool can) {
        if (!can) dashTimeCounter = configuration.DashCooldown;
        CanDash = can;
    }
    public bool IsGrounded => Collisions.IsGrounded;
    public bool IsCeiled => Collisions.IsCeiled;
    public bool IsFrontBlocked => Collisions.HasSomethingInFrontNotPusheable;
    public bool CanPushSomething => Collisions.HasSomethingInFrontPusheable;
    public bool CanJumpCoyote => !IsGrounded && coyoteTimeCounter < configuration.CoyoteTime;

    private bool groundedLastFrame;
    private float coyoteTimeCounter;
    private float dashTimeCounter;
    private string currentAnimation;
    private bool isAttacking;
    public void SetIsAttacking(bool isAttacking) => this.isAttacking = isAttacking;
    private bool isDashing;
    public void SetIsDashing(bool isDashing) => this.isDashing = isDashing;

    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
        configuration.CalculateValues();
        TrailRenderer.enabled = false;
        ChangeAnimation(Constants.PLAYER_IDLE_ANIM);
        MovementParticles.Stop();
    }

    private void Update() {
        stateMachine.Step();
        CheckFlip();
        ApplyGravity();
        CheckCoyoteTime();
        CheckCanDash();
        // TODO: TEST - REMOVE THIS
        // ######
        if (EPInputManager.Instance.ResetInput) UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        // ######
    }

    private void FixedUpdate() {
        stateMachine.PhysicsStep();
    }

    private void LateUpdate() {
        stateMachine.LateStep();
    }

    public void TryMoveX(float motion) {
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
    }

    private void CheckFlip() {
        if (EPInputManager.Instance.MoveInput == 0 || isAttacking || isDashing) return;
        Vector3 scale = transform.localScale;
        scale.x = EPInputManager.Instance.MoveInput > 0 ? 1 : -1;
        transform.localScale = scale;
    }

    private void CheckCoyoteTime() {
        if (!IsGrounded && groundedLastFrame) coyoteTimeCounter = 0f;
        if (!IsGrounded) {
            coyoteTimeCounter += Time.deltaTime;
            if (coyoteTimeCounter > configuration.CoyoteTime) coyoteTimeCounter = configuration.CoyoteTime;
        }
        else coyoteTimeCounter = 0f;
    }

    private void CheckCanDash() {
        if (dashTimeCounter > 0f) dashTimeCounter -= Time.deltaTime;
        if (IsGrounded && dashTimeCounter <= 0f) SetCanDash(true);
    }

    public void ChangeAnimation(string nextState) {
        if (currentAnimation == nextState) return;
        Animator.Play(nextState);
        currentAnimation = nextState;
    }
}
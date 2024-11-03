using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerConfiguration configuration;
    [field : SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field : SerializeField] public Animator Animator { get; private set; }
    [Header("Gameplay")]
    [SerializeField] private AreaChecker groundCheck;
    public bool IsGrounded => groundCheck.IsOverlapping();

    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
    }

    private void Update() {
        stateMachine.Step();
        CheckFlip();
        ApplyGravity();
        UpdateAnimator();
    }

    private void FixedUpdate() {
        stateMachine.PhysicsStep();
    }

    private void LateUpdate() {
        stateMachine.LateStep();    
    }

    private void ApplyGravity() {
        if (IsGrounded) {
            if (Rigidbody2D.linearVelocityY < 0f) Rigidbody2D.linearVelocityY = 0f;
        }
        else Rigidbody2D.linearVelocityY -= 1.25f * Time.deltaTime;
    }

    private void UpdateAnimator() {
        Animator.SetFloat(Constants.PLAYER_ANIMATOR_X_SPEED, Mathf.Clamp01(Mathf.Abs(Rigidbody2D.linearVelocityX)));
    }

    private void CheckFlip(){
        if (Rigidbody2D.linearVelocityX == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = Rigidbody2D.linearVelocityX > 0 ? 1 : -1;            
        transform.localScale = scale;
    }
}
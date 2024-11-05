using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerConfiguration configuration;
    [field : SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field : SerializeField] public Animator Animator { get; private set; }
    [Header("Gameplay")]
    [SerializeField] private AreaChecker groundCheck;
    [SerializeField] private AreaChecker ceilCheck;
    public bool IsGrounded => groundCheck.IsOverlapping();
    public bool IsCeiled => ceilCheck.IsOverlapping();

    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
    }

    private void Update() {

        Debug.Log(IsCeiled);

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
        if (IsGrounded) {
            if (Rigidbody2D.linearVelocityY < 0f) Rigidbody2D.linearVelocityY = 0f;
        }
        else Rigidbody2D.linearVelocityY += configuration.GravityForce * Time.deltaTime;
    }

    private void CheckFlip() {
        if (Rigidbody2D.linearVelocityX == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = Rigidbody2D.linearVelocityX > 0 ? 1 : -1;            
        transform.localScale = scale;
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerConfiguration configuration;
    [field : SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field : SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Transform AttackPoint { get; private set; }
    [Header("Gameplay")]
    [SerializeField] private AreaChecker groundCheck;
    [SerializeField] private AreaChecker ceilCheck;
    [SerializeField] private AreaChecker frontalCheck;
    public bool IsGrounded => groundCheck.IsOverlapping();
    public bool IsCeiled => ceilCheck.IsOverlapping();
    public bool IsFrontBlocked => frontalCheck.IsOverlapping();
    public bool CanMove { get; private set; }

    public void SetCanMove (bool canMove) => CanMove = canMove;

    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
        SetCanMove(true);
    }

    private void Update() {

        Debug.Log(IsCeiled);

        stateMachine.Step();
        CheckCanMove();
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

    private void CheckCanMove() => SetCanMove(!IsFrontBlocked);
}
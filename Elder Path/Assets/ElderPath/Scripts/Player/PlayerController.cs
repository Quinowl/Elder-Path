using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerConfiguration configuration;
    [field : SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [SerializeField] private AreaChecker groundCheck;
    public bool IsGrounded => groundCheck.IsOverlapping();

    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
    }

    private void Update() {
        ApplyGravity();
        stateMachine.Step();
    }

    private void FixedUpdate() {
        stateMachine.PhysicsStep();
    }

    private void LateUpdate() {
        stateMachine.LateStep();    
    }

    private void ApplyGravity() {
        
    }
}
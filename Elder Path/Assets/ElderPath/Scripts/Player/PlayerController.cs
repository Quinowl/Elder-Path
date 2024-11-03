using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerConfiguration configuration;
    [field : SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    
    private void Start() {
        stateMachine.ConfigureStateMachine(configuration, this);
        stateMachine.Initialize();
    }

    private void Update() {
        stateMachine.Step();
    }

    private void FixedUpdate() {
        stateMachine.PhysicsStep();
    }

    private void LateUpdate() {
        stateMachine.LateStep();    
    }
}
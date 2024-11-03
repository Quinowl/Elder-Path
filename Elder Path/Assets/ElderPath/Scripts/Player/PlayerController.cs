using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private PlayerStateMachine stateMachine;

    private void Start() {
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
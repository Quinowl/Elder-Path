using UnityEngine;

public class CharacterMovementState : PlayerState {
    private float currentSpeed;
    public override void StateEnter() {
        currentSpeed = 0f;
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput == 0) stateMachine.ChangeState(typeof(CharacterIdleState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
        float targetSpeed = EPInputManager.Instance.MoveInput * configuration.MaxSpeed;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, configuration.AccelerationRate * Time.fixedDeltaTime);
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = currentSpeed;
    }

    public override void StateStep() {
    }   
}
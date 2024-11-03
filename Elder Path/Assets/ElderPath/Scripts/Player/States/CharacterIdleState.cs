using UnityEngine;

public class CharacterIdleState : PlayerState {

    private float currentSpeed;

    public override void StateEnter() {
        currentSpeed = stateMachine.PlayerController.Rigidbody2D.linearVelocityX;
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput != 0) stateMachine.SetState(typeof(CharacterMovementState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, configuration.DeccelerationRate * Time.fixedDeltaTime);
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = currentSpeed;
    }

    public override void StateStep() {
    }
}
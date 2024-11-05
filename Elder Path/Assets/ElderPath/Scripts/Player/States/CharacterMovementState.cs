using UnityEngine;

public class CharacterMovementState : PlayerState {
    private float currentSpeed;
    public override void StateEnter() {
        currentSpeed = 0f;
        stateMachine.PlayerController.Animator.SetFloat(Constants.PLAYER_ANIMATOR_X_SPEED, 1f);
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput == 0) stateMachine.SetState(typeof(CharacterIdleState));
        if (EPInputManager.Instance.JumpInput && stateMachine.PlayerController.IsGrounded) stateMachine.SetState(typeof(CharacterJumpState));            
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
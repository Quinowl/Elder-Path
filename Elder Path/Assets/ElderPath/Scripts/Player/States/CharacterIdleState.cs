using UnityEngine;

public class CharacterIdleState : PlayerState {
    private float currentSpeed;

    public override void StateEnter() {
        currentSpeed = stateMachine.PlayerController.Rigidbody2D.linearVelocityX;
        stateMachine.PlayerController.Animator.SetFloat(Constants.PLAYER_ANIMATOR_X_SPEED, 0f);
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput != 0) stateMachine.SetState(typeof(CharacterMovementState));
        if (EPInputManager.Instance.JumpInputPressed && stateMachine.PlayerController.IsGrounded && !stateMachine.PlayerController.IsCeiled) stateMachine.SetState(typeof(CharacterJumpTransition));
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
using UnityEngine;

public class CharacterFallingState : PlayerState {

    public override void StateEnter() {
        stateMachine.PlayerController.ChangeAnimation(Constants.PLAYER_FALLING_ANIM);
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.DashInput && stateMachine.PlayerController.CanDash) stateMachine.SetState(typeof(CharacterDashState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        stateMachine.PlayerController.TryMoveX(EPInputManager.Instance.MoveInput * configuration.MaxSpeed);
        if (stateMachine.PlayerController.IsGrounded) stateMachine.SetState(stateMachine.PlayerController.Rigidbody2D.linearVelocityX > 0f ? typeof(CharacterMovementState) : typeof(CharacterIdleState));
    }
}
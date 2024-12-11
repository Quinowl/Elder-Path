using UnityEngine;

public class CharacterPushingState : PlayerState {

    public override void StateEnter() {
        stateMachine.PlayerController.ChangeAnimation(Constants.PlayerAnimations.PUSH_ANIM);
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        stateMachine.PlayerController.TryMoveX(EPInputManager.Instance.MoveInput * configuration.PushingSpeed);
        if (!stateMachine.PlayerController.CanPushSomething) stateMachine.SetState(typeof(CharacterMovementState));
        if (EPInputManager.Instance.MoveInput == 0f) stateMachine.SetState(typeof(CharacterIdleState));
        if (EPInputManager.Instance.JumpInputPressed
            && stateMachine.PlayerController.IsGrounded
            && !stateMachine.PlayerController.IsCeiled) {
            stateMachine.SetState(typeof(CharacterJumpState));
        }
    }
}
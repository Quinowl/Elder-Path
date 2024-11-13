using UnityEngine;

public class CharacterJumpTransition : PlayerState {

    private float counter;

    public override void StateEnter() {
        stateMachine.PlayerController.Animator.SetTrigger(Constants.PLAYER_ANIMATOR_JUMP_TRIGGER);
        counter = 0.03f;
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
        counter -= Time.deltaTime;
        if (counter <= 0f) {
            stateMachine.SetState(EPInputManager.Instance.JumpInputHeld && !stateMachine.PlayerController.IsCeiled ? typeof(CharacterJumpState) : typeof(CharacterIdleState));
        }
    }
}
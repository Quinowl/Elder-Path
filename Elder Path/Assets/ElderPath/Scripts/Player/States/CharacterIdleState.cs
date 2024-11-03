using UnityEngine;

public class CharacterIdleState : PlayerState {

    public override void StateEnter() {
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput != 0) stateMachine.ChangeState(typeof(CharacterMovementState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
    }
}
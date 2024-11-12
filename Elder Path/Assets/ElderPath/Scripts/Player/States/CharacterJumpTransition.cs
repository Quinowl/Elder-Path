using UnityEngine;

public class CharacterJumpTransition : PlayerState {
    public override void StateEnter() {
        Debug.Log("Transicionamos a jump");
        // stateMachine.PlayerController.Animator.SetTrigger();
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
    }
}
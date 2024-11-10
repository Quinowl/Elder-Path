using UnityEngine;

public class CharacterAttackState : PlayerState {

    public override void StateEnter() {
        Debug.Log("Estamos en el estado de ataque");
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

    private bool HasHit() {
        
        return false;
    }
}
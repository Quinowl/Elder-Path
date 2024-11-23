using UnityEngine;

public class CharacterDashState : PlayerState {
    public override void StateEnter() {
        //TODO: Impulso en el eje X
        stateMachine.PlayerController.TryMoveX(configuration.DashForce);
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
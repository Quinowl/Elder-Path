using UnityEngine;

public class CharacterLandingState : PlayerState {

    private float timeCounter;

    public override void StateEnter() {
        stateMachine.PlayerController.ChangeAnimation(Constants.PLAYER_LAND_ANIM);
        stateMachine.PlayerController.Rigidbody2D.linearVelocity = Vector2.zero;
        timeCounter = 0.05f;
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
        timeCounter -= Time.deltaTime;
        if (timeCounter <= 0f) stateMachine.SetState(EPInputManager.Instance.MoveInput != 0 ? typeof(CharacterMovementState) : typeof(CharacterIdleState));
    }
}
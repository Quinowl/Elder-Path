using UnityEngine;

public class CharacterDashState : PlayerState {

    private float startTime;

    public override void StateEnter() {
        startTime = Time.time;
        stateMachine.PlayerController.TrailRenderer.enabled = true;
        stateMachine.PlayerController.Rigidbody2D.linearVelocityY *= 0.3f;
        stateMachine.PlayerController.TryMoveX(configuration.DashForce * stateMachine.PlayerController.transform.localScale.x);
        stateMachine.PlayerController.SetCanDash(false);
    }

    public override void StateExit() {
        //TODO: Change these magic numbers
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX *= 0.25f;
        stateMachine.PlayerController.TrailRenderer.enabled = false;
    }

    public override void StateInputs() {
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        if (Time.time > startTime + configuration.DashTime) stateMachine.SetState(typeof(CharacterMovementState));
    }
}
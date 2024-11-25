using UnityEngine;

public class CharacterDashState : PlayerState {

    private float startTime;

    public override void StateEnter() {
        stateMachine.PlayerController.SetCanDash(false);
        startTime = Time.time;
        stateMachine.PlayerController.TrailRenderer.enabled = true;
        stateMachine.PlayerController.Rigidbody2D.linearVelocityY *= 0.3f;
        stateMachine.PlayerController.TryMoveX(configuration.DashForce * stateMachine.PlayerController.transform.localScale.x);
        stateMachine.PlayerController.SetIsDashing(true);
    }

    public override void StateExit() {
        //TODO: Change these magic numbers
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX *= 0.25f;
        stateMachine.PlayerController.TrailRenderer.enabled = false;
        stateMachine.PlayerController.SetIsDashing(false);
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.AttackInput && stateMachine.PlayerController.CanAttack) stateMachine.SetState(typeof(CharacterAttackState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        if (Time.time > startTime + configuration.DashTime) stateMachine.SetState(typeof(CharacterMovementState));
    }
}
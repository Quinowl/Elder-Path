using UnityEngine;

public class CharacterIdleState : PlayerState {
    private float currentSpeed;

    public override void StateEnter() {
        currentSpeed = stateMachine.PlayerController.Rigidbody2D.linearVelocityX;
        stateMachine.PlayerController.ChangeAnimation(Constants.PLAYER_IDLE_ANIM);
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput != 0) stateMachine.SetState(typeof(CharacterMovementState));
        if (EPInputManager.Instance.JumpInputPressed && stateMachine.PlayerController.IsGrounded && !stateMachine.PlayerController.IsCeiled) stateMachine.SetState(typeof(CharacterJumpState));
        if (EPInputManager.Instance.DashInput && stateMachine.PlayerController.CanDash && !stateMachine.PlayerController.IsFrontBlocked) stateMachine.SetState(typeof(CharacterDashState));
        if (EPInputManager.Instance.AttackInput && stateMachine.PlayerController.CanAttack) stateMachine.SetState(typeof(CharacterAttackState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, configuration.DeccelerationRate * Time.deltaTime);
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = currentSpeed;
    }
}
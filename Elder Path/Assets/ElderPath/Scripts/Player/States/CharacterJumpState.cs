using UnityEngine;

public class CharacterJumpState : PlayerState {

    private float groundCheckDelay;

    public override void StateEnter() {
        groundCheckDelay = Constants.PLAYER_GROUND_CHECK_DELAY_AFTER_JUMP;
        stateMachine.PlayerController.TryMoveX(EPInputManager.Instance.MoveInput * configuration.MaxSpeed);
        stateMachine.PlayerController.Rigidbody2D.linearVelocityY = configuration.JumpForce;
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        // Regulable jump
        if (EPInputManager.Instance.JumpInputReleased && stateMachine.PlayerController.Rigidbody2D.linearVelocityY > 0f) stateMachine.PlayerController.Rigidbody2D.linearVelocityY *= 0.5f;
        if (EPInputManager.Instance.DashInput && stateMachine.PlayerController.CanDash) stateMachine.SetState(typeof(CharacterDashState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        stateMachine.PlayerController.TryMoveX(EPInputManager.Instance.MoveInput * configuration.MaxSpeed);
        if (stateMachine.PlayerController.IsCeiled) stateMachine.PlayerController.Rigidbody2D.linearVelocityY = -0.25f * configuration.JumpForce;
        groundCheckDelay -= Time.deltaTime;
        if (groundCheckDelay > 0f) return;
        if (!stateMachine.PlayerController.IsGrounded) return;
        if (stateMachine.PlayerController.Rigidbody2D.linearVelocity != Vector2.zero) stateMachine.SetState(typeof(CharacterMovementState));
        else stateMachine.SetState(typeof(CharacterIdleState));
    }
}
using UnityEngine;

public class CharacterMovementState : PlayerState {

    private float currentSpeed;
    private float TargetSpeed => EPInputManager.Instance.MoveInput * configuration.MaxSpeed;

    public override void StateEnter() {
        currentSpeed = stateMachine.LastState is CharacterIdleState ? 0f : stateMachine.PlayerController.Rigidbody2D.linearVelocityX;
        stateMachine.PlayerController.ChangeAnimation(Constants.PLAYER_RUN_ANIM);
        stateMachine.PlayerController.MovementParticles.Play();
    }

    public override void StateExit() {
        stateMachine.PlayerController.MovementParticles.Stop();
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput == 0 && stateMachine.PlayerController.IsGrounded) stateMachine.SetState(typeof(CharacterIdleState));
        if (EPInputManager.Instance.JumpInputPressed
            && (stateMachine.PlayerController.IsGrounded || stateMachine.PlayerController.CanJumpCoyote)
            && !stateMachine.PlayerController.IsCeiled) {
            stateMachine.SetState(typeof(CharacterJumpState));
        }
        if (stateMachine.PlayerController.Rigidbody2D.linearVelocityY < 0f) stateMachine.SetState(typeof(CharacterFallingState));
        if (EPInputManager.Instance.AttackInput && stateMachine.PlayerController.CanAttack) stateMachine.SetState(typeof(CharacterAttackState));
        if (EPInputManager.Instance.DashInput && stateMachine.PlayerController.CanDash && !stateMachine.PlayerController.IsFrontBlocked) stateMachine.SetState(typeof(CharacterDashState));
        if (stateMachine.PlayerController.CanPushSomething && EPInputManager.Instance.MoveInput != 0) stateMachine.SetState(typeof(CharacterPushingState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        // If direction is changed, conserve current speed,
        if (IsDirectionChanged(TargetSpeed)) {
            currentSpeed = Mathf.Sign(TargetSpeed) * Mathf.Max(Mathf.Abs(currentSpeed), configuration.MinSpeedOnDirectionChange);
        }
        // else it will accelerate to target speed.
        else {
            if (!stateMachine.PlayerController.IsFrontBlocked) currentSpeed = Mathf.MoveTowards(currentSpeed, TargetSpeed, configuration.AccelerationRate * Time.deltaTime);
            else currentSpeed = 0f;
        }
        stateMachine.PlayerController.TryMoveX(currentSpeed);
    }

    // The value 0.1f is a magic number that marks a small sensitivity threshold.
    private bool IsDirectionChanged(float targetSpeed) => Mathf.Sign(currentSpeed) != Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > Mathf.Epsilon;
}
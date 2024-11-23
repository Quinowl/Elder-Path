using UnityEngine;

public class CharacterMovementState : PlayerState {

    private float currentSpeed;
    private float TargetSpeed => EPInputManager.Instance.MoveInput * configuration.MaxSpeed;

    public override void StateEnter() {
        currentSpeed = (stateMachine.LastState is CharacterJumpState || stateMachine.LastState is CharacterDashState) ? stateMachine.PlayerController.Rigidbody2D.linearVelocityX : 0f;
        stateMachine.PlayerController.Animator.SetFloat(Constants.PLAYER_ANIMATOR_X_SPEED, 1f);
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput == 0) stateMachine.SetState(typeof(CharacterIdleState));
        if (EPInputManager.Instance.JumpInputPressed
            && (stateMachine.PlayerController.IsGrounded || stateMachine.PlayerController.CanJumpCoyote)
            && !stateMachine.PlayerController.IsCeiled) {
            stateMachine.SetState(typeof(CharacterJumpTransition));
        }
        // if (EPInputManager.Instance.AttackInput) stateMachine.SetState(typeof(CharacterAttackState));
        if (EPInputManager.Instance.DashInput && stateMachine.PlayerController.CanDash) stateMachine.SetState(typeof(CharacterDashState));
        if (stateMachine.PlayerController.CanPushSomething && EPInputManager.Instance.MoveInput != 0) stateMachine.SetState(typeof(CharacterPushingState));
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
    }

    public override void StateStep() {
        // If direction is changed, conserve current speed,
        if (IsDirectionChanged(TargetSpeed)) {
            currentSpeed = TargetSpeed;
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
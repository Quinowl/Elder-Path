using UnityEngine;

public class CharacterMovementState : PlayerState {
    private float currentSpeed;
    public override void StateEnter() {
        currentSpeed = (stateMachine.LastState is CharacterJumpState) ? stateMachine.PlayerController.Rigidbody2D.linearVelocityX : 0f;
        stateMachine.PlayerController.Animator.SetFloat(Constants.PLAYER_ANIMATOR_X_SPEED, 1f);
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.MoveInput == 0) stateMachine.SetState(typeof(CharacterIdleState));
        if (EPInputManager.Instance.JumpInput && stateMachine.PlayerController.IsGrounded && !stateMachine.PlayerController.IsCeiled) stateMachine.SetState(typeof(CharacterJumpState));
        if (EPInputManager.Instance.AttackInput) stateMachine.SetState(typeof(CharacterAttackState));
        // if (EPInputManager.Instance.DashInput) stateMachine.SetState(typeof(CharacterDashState));            
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
        if (!stateMachine.PlayerController.CanMove) return;
        float targetSpeed = EPInputManager.Instance.MoveInput * configuration.MaxSpeed;
        // If direction is changed, conserve current speed,
        if (IsDirectionChanged(targetSpeed)) currentSpeed = targetSpeed;      
        // else it will accelerate to target speed.
        else currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, configuration.AccelerationRate * Time.fixedDeltaTime);
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = currentSpeed;
    }

    public override void StateStep() {
    }   

    // The value 0.1f is a magic number that marks a small sensitivity threshold.
    private bool IsDirectionChanged(float targetSpeed) => Mathf.Sign(currentSpeed) != Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.1f;
}
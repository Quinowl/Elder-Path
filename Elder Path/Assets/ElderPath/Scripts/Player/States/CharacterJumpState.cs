using UnityEngine;

public class CharacterJumpState : PlayerState {
    
    public override void StateEnter() {
        Debug.Log("Jump?");
        stateMachine.PlayerController.Rigidbody2D.linearVelocityY = configuration.JumpForce;
        // Debemos mover al jugador mientras salta y tenemos que ir mirando cuándo cae y demás
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = EPInputManager.Instance.MoveInput * configuration.MaxSpeed;
    }

    public override void StateStep() {
        if (stateMachine.PlayerController.IsGrounded) {
            if (stateMachine.PlayerController.Rigidbody2D.linearVelocity != Vector2.zero) stateMachine.SetState(typeof(CharacterMovementState));
            else stateMachine.SetState(typeof(CharacterIdleState));
        }
    }  
}
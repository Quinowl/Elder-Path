using UnityEngine;

public class CharacterJumpState : PlayerState {
    
    private float groundCheckDelay;
    //TODO: I dont like a LayerMask here, fix this
    [SerializeField] private LayerMask groundLayer;

    public override void StateEnter() {
        groundCheckDelay = configuration.GroundCheckDelayAfterJump;
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = EPInputManager.Instance.MoveInput * configuration.MaxSpeed;
        stateMachine.PlayerController.Rigidbody2D.linearVelocityY = configuration.JumpForce;
    }

    public override void StateExit() {
    }

    public override void StateInputs() {
        if (EPInputManager.Instance.JumpInputReleased && stateMachine.PlayerController.Rigidbody2D.linearVelocityY > 0f) {
            stateMachine.PlayerController.Rigidbody2D.linearVelocityY *= 0.5f;
        }
    }

    public override void StateLateStep() {
    }

    public override void StatePhysicsStep() {
        stateMachine.PlayerController.Rigidbody2D.linearVelocityX = EPInputManager.Instance.MoveInput * configuration.MaxSpeed;
    }

    public override void StateStep() {
        if (stateMachine.PlayerController.IsCeiled) stateMachine.PlayerController.Rigidbody2D.linearVelocityY = -0.25f * configuration.JumpForce;             
        groundCheckDelay -= Time.deltaTime;
        if (groundCheckDelay > 0f) return;
        if (stateMachine.PlayerController.IsGrounded) {
            //TODO: Fix for player, now he can go inside floor, we have to fix and calculate the distance to go avoid terrain
            Vector2 position = stateMachine.PlayerController.Rigidbody2D.position;
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, .01f, groundLayer);
            if (hit.collider != null) {
                float penetrationDepth = Mathf.Max(0f, .01f - hit.distance);
                if (penetrationDepth > 0f) {
                    position.y += penetrationDepth;
                    stateMachine.PlayerController.Rigidbody2D.position = position;
                }
            }
            if (stateMachine.PlayerController.Rigidbody2D.linearVelocity != Vector2.zero) stateMachine.SetState(typeof(CharacterMovementState));
            else stateMachine.SetState(typeof(CharacterIdleState));
        }
    }  
}
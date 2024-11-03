using UnityEngine;

public abstract class PlayerState : MonoBehaviour {
    protected PlayerStateMachine stateMachine;
    public void SetStateMachine(PlayerStateMachine stateMachine) => this.stateMachine = stateMachine;
    public abstract void StateInputs();
    public abstract void StateEnter();
    public abstract void StateStep();
    public abstract void StatePhysicsStep();
    public abstract void StateLateStep();
    public abstract void StateExit();
}
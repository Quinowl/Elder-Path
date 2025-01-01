using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;
    protected PlayerConfiguration configuration;
    public void Configure(PlayerStateMachine stateMachine, PlayerConfiguration configuration)
    {
        this.stateMachine = stateMachine;
        this.configuration = configuration;
    }
    public abstract void StateInputs();
    public abstract void StateEnter();
    public abstract void StateStep();
    public abstract void StatePhysicsStep();
    public abstract void StateLateStep();
    public abstract void StateExit();
}
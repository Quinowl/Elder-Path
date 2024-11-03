using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour {

    [SerializeField] private PlayerState[] states;
    public PlayerState CurrentState { get; private set; }

    public void Initialize() {
        if (states == null || states.Length <= 0) {
            Debug.LogError("Player state machine has not states added.");
            return;
        }
        foreach (PlayerState state in states) state.SetStateMachine(this);
        ChangeState(states[0].GetType());
    }

    public void Step() {
        if (CurrentState == null) return;
        CurrentState.StateInputs();
        CurrentState.StateStep();
    }

    public void PhysicsStep() {
        if (CurrentState == null) return;
        CurrentState.StatePhysicsStep();
    }

    public void LateStep() {
        if (CurrentState == null) return;
        CurrentState.StateLateStep();
    }

    public void ChangeState(Type nextStateType) {
        PlayerState nextState = GetStateByType(nextStateType);
        if (nextState == null || nextState == CurrentState) return;
        CurrentState?.StateExit();
        CurrentState = nextState;
        CurrentState.StateEnter();
    }

    private PlayerState GetStateByType(Type stateType){
        for (int i = 0; i < states.Length; i++) {
            if (states[i].GetType() == stateType) return states[i];
        }
        return null;
    }
}
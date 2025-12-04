// PlayerStateMachine.cs
using UnityEngine;
public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    
    public void Initialize(PlayerState startingState)
    {
        Debug.Log($"[STATE] Initialized to {startingState.Name}");
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        Debug.Log($"[STATE] {CurrentState.Name} -> {newState.Name}");
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
        
    }
}

// PlayerState.cs
using UnityEngine;

public abstract class PlayerState
{
    protected readonly Player player;
    protected readonly PlayerStateMachine stateMachine;

    public virtual string Name => GetType().Name;

    protected PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()          { }
    public virtual void HandleInput()    { }
    public virtual void LogicUpdate()    { }
    public virtual void PhysicsUpdate()  { }
    public virtual void Exit()           { }
}

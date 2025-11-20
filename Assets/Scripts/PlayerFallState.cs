// PlayerFallState.cs
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        if (player.animator != null)
        {
            player.animator.Play("Player_Fall"); // placeholder
        }

        // Stronger gravity when falling
        player.ApplyGravityMultiplier(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.IsGrounded)
        {
            if (Mathf.Abs(player.MoveInput) > 0.01f)
                stateMachine.ChangeState(player.MoveState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.ApplyHorizontalMovement();
        player.FaceMovementDirection();
    }

    public override void Exit()
    {
        base.Exit();
        // Restore normal gravity
        player.ApplyGravityMultiplier(false);
    }
}

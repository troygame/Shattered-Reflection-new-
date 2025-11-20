// PlayerJumpState.cs
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        player.DoJump();

        if (player.animator != null)
        {
            player.animator.Play("Player_Jump"); // placeholder
        }

        // Normal gravity on the way up
        player.ApplyGravityMultiplier(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // When we start falling, switch to Fall state
        if (player.rb.linearVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.ApplyHorizontalMovement();
        player.FaceMovementDirection();
    }
}

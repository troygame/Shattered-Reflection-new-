// PlayerMoveState.cs
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        if (player.animator != null)
        {
            player.animator.Play("Player_Run"); // placeholder
        }

        player.ApplyGravityMultiplier(false);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (player.JumpPressed && player.IsGrounded)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (Mathf.Abs(player.MoveInput) <= 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!player.IsGrounded)
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

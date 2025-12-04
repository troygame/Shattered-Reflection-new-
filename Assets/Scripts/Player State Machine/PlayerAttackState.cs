using UnityEngine;

public class PlayerAttackState: PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine)
        : base(player, stateMachine) { }

    private float timer = 0;

    public override void Enter()
    {
        base.Enter();


        if (player.animator != null)
        {
            player.animator.Play("Player_Attack"); // placeholder
        }
        timer = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer += Time.deltaTime;
        // When we start falling, switch to Fall state
        if (timer>1f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.FaceMovementDirection();
    }
}

using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animatorBoolName) : base(_player, _stateMachine, _animatorBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {   
        base.Update();
    }
}

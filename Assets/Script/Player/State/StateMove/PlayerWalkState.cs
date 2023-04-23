using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerWalkState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        //“ü—Í‚ª–³‚¯‚ê‚ÎIdleó‘Ô‚ÖˆÚs
        if (_player.Input.GetMoveDir() == new Vector2(0f, 0f))
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
        }

        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }

        _player.Walk();
    }

    public void Exit()
    {

    }
}

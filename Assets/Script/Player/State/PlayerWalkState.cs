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
        if (_player.InputH == 0 && _player.InputV == 0)
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
        }

        _player.Walk();
    }

    public void Exit()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerIdleState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        if (_player.InputH != 0 || _player.InputV != 0)
        {
            _player.StateMachine.TransitionState(new PlayerWalkState(_player));
        }
    }

    public void Exit()
    {

    }
}

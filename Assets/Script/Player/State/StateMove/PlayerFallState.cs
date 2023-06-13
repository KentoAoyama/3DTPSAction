using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerFallState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        //Dashの入力があればDash状態へ遷移
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }

        //接地をしたらIdle状態に遷移
        if (_player.IsGround())
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
        }

        _player.FallMove();
    }

    public void Exit()
    {

    }
}


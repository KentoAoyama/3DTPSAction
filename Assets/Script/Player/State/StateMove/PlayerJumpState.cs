using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerJumpState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.Animation.JumpParameterSet(true);
    }

    public void Update()
    {
        // Dash�̓��͂������DashState�ɑJ��
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
            return;
        }

        //�W�����v�̏������I��������FallState�ɑJ��
        if (!_player.IsJump())
        {
            _player.StateMachine.TransitionState(new PlayerFallState(_player));
            return;
        }

        //_player.Move();
        _player.Jump();
    }

    public void Exit()
    {
        _player.Animation.JumpParameterSet(false);
    }
}

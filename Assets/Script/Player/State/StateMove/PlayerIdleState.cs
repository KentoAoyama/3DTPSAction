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
        // �ړ��̓��͂������MoveState�ɑJ��
        if (_player.Input.GetMoveDir() != new Vector2(0f, 0f))
        {
            _player.StateMachine.TransitionState(new PlayerWalkState(_player));
        }
        // Dash�̓��͂������DashState�ɑJ��
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }
    }

    public void Exit()
    {

    }
}

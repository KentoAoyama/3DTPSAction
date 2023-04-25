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
        // 移動の入力があればMoveStateに遷移
        if (_player.Input.GetMoveDir() != new Vector2(0f, 0f))
        {
            _player.StateMachine.TransitionState(new PlayerWalkState(_player));
        }
        // Dashの入力があればDashStateに遷移
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }
    }

    public void Exit()
    {

    }
}

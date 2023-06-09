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
        Debug.Log("IdleState");
    }

    public void Update()
    {
        // 移動の入力があればMoveStateに遷移
        if (_player.Input.GetMoveDir() != Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerWalkState(_player));
        }

        // Dashの入力があればDashStateに遷移
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }

        //地面に接触していなければFall状態へ遷移
        if (!_player.IsGround())
        {
            _player.StateMachine.TransitionState(new PlayerFallState(_player));
        }

        _player.Walk();
    }

    public void Exit()
    {

    }
}

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
        if (_player.Input.GetMoveDir() != Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerWalkState(_player));
            return;
        }

        // Dashの入力があればDashStateに遷移
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
            return;
        }

        //Jumpの入力があればJumpStateに遷移
        if (_player.Input.GetJump())
        {
            _player.StateMachine.TransitionState(new PlayerJumpState(_player));
            return;
        }

        //地面に接触していなければFall状態へ遷移
        if (!_player.IsGround())
        {
            _player.StateMachine.TransitionState(new PlayerFallState(_player));
            return;
        }

        _player.Attack();
        _player.Move();
    }

    public void Exit()
    {

    }
}

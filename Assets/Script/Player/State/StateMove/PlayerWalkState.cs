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
        Debug.Log("WalkState");

    }

    public void Update()
    {
        //入力が無ければIdle状態へ遷移
        if (_player.Input.GetMoveDir() == new Vector2(0f, 0f))
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
        }

        //Dashの入力があればDash状態へ遷移
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

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
        //���͂��������Idle��Ԃ֑J��
        if (_player.Input.GetMoveDir() == Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
        }

        //Dash�̓��͂������Dash��Ԃ֑J��
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }

        //Jump�̓��͂������JumpState�ɑJ��
        if (_player.Input.GetJump())
        {
            _player.StateMachine.TransitionState(new PlayerJumpState(_player));
        }

        //�n�ʂɐڐG���Ă��Ȃ����Fall��Ԃ֑J��
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

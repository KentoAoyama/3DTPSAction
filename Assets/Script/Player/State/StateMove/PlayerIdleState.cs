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
        // ˆÚ“®‚Ì“ü—Í‚ª‚ ‚ê‚ÎMoveState‚É‘JˆÚ
        if (_player.Input.GetMoveDir() != Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerWalkState(_player));
        }

        // Dash‚Ì“ü—Í‚ª‚ ‚ê‚ÎDashState‚É‘JˆÚ
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }

        //Jump‚Ì“ü—Í‚ª‚ ‚ê‚ÎJumpState‚É‘JˆÚ
        if (_player.Input.GetJump())
        {
            _player.StateMachine.TransitionState(new PlayerJumpState(_player));
        }

        //’n–Ê‚ÉÚG‚µ‚Ä‚¢‚È‚¯‚ê‚ÎFalló‘Ô‚Ö‘JˆÚ
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

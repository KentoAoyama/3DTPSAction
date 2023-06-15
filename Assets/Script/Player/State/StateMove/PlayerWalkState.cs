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
        //“ü—Í‚ª–³‚¯‚ê‚ÎIdleó‘Ô‚Ö‘JˆÚ
        if (_player.Input.GetMoveDir() == Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
            return;
        }

        //Dash‚Ì“ü—Í‚ª‚ ‚ê‚ÎDashó‘Ô‚Ö‘JˆÚ
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
            return;
        }

        //Jump‚Ì“ü—Í‚ª‚ ‚ê‚ÎJumpState‚É‘JˆÚ
        if (_player.Input.GetJump())
        {
            _player.StateMachine.TransitionState(new PlayerJumpState(_player));
            return;
        }

        //’n–Ê‚ÉÚG‚µ‚Ä‚¢‚È‚¯‚ê‚ÎFalló‘Ô‚Ö‘JˆÚ
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

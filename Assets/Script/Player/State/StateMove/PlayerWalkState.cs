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
        //“ü—Í‚ª–³‚¯‚ê‚ÎIdleó‘Ô‚Ö‘JˆÚ
        if (_player.Input.GetMoveDir() == Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
        }

        //Dash‚Ì“ü—Í‚ª‚ ‚ê‚ÎDashó‘Ô‚Ö‘JˆÚ
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
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

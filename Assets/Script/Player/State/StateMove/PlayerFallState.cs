using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerFallState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.Animation.IsGroundParameterSet(false);
    }

    public void Update()
    {
        //Dash‚Ì“ü—Í‚ª‚ ‚ê‚ÎDashó‘Ô‚Ö‘JˆÚ
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
            return;
        }

        //Ú’n‚ğ‚µ‚½‚çIdleó‘Ô‚É‘JˆÚ
        if (_player.IsGround())
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
            return;
        }

        _player.Attack();
        _player.Fall();
    }

    public void Exit()
    {

    }
}


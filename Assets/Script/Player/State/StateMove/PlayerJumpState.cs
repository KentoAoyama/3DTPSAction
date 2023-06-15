using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerJumpState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {
        _player.Animation.JumpParameterSet(true);
    }

    public void Update()
    {
        // Dash‚Ì“ü—Í‚ª‚ ‚ê‚ÎDashState‚É‘JˆÚ
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
            return;
        }

        //ƒWƒƒƒ“ƒv‚Ìˆ—‚ªI—¹‚µ‚½‚çFallState‚É‘JˆÚ
        if (!_player.IsJump())
        {
            _player.StateMachine.TransitionState(new PlayerFallState(_player));
            return;
        }

        //_player.Move();
        _player.Jump();
    }

    public void Exit()
    {
        _player.Animation.JumpParameterSet(false);
    }
}

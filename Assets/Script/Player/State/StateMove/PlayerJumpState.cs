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
        
    }

    public void Update()
    {
        _player.Jump();

        // Dash‚Ì“ü—Í‚ª‚ ‚ê‚ÎDashState‚É‘JˆÚ
        if (_player.Input.GetDash())
        {
            _player.StateMachine.TransitionState(new PlayerDashState(_player));
        }

        //ƒWƒƒƒ“ƒv‚Ìˆ—‚ªI—¹‚µ‚½‚çFallState‚É‘JˆÚ
        if (!_player.IsJump())
        {
            _player.StateMachine.TransitionState(new PlayerFallState(_player));
        }
    }

    public void Exit()
    {

    }
}

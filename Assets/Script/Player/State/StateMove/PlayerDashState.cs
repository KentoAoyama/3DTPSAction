using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerDashState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerDashState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {
        DashAsync(default).Forget();
    }

    /// <summary>
    /// Dash‚Ìˆ—‚ğAsync‚Ås‚¤
    /// </summary>
    private async UniTask DashAsync(CancellationToken token)
    {
        await _player.DashAsync(token);

        _player.StateMachine.TransitionState(new PlayerIdleState(_player));
    }

    public void Update()
    {
        _player.Dash();
    }

    public void Exit()
    {

    }
}

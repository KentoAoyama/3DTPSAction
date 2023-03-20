using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private IPlayerState _currentState;
    public IPlayerState CurrentState => _currentState;

    public void Initialized(IPlayerState state)
    {
        _currentState = state;
        state.Enter();
    }

    /// <summary>
    /// Stateを変更する際に呼びだすメソッド
    /// </summary>
    /// <param name="nextState">変更するState</param>
    public void TransitionState(IPlayerState nextState)
    {
        if (_currentState == nextState) return;

        _currentState.Exit();
        _currentState = nextState;
        nextState.Enter();
    }

    /// <summary>
    /// 現在のStateのUpdate処理を行うメソッド
    /// </summary>
    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }
}
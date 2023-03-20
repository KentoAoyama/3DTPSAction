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
    /// State��ύX����ۂɌĂт������\�b�h
    /// </summary>
    /// <param name="nextState">�ύX����State</param>
    public void TransitionState(IPlayerState nextState)
    {
        if (_currentState == nextState) return;

        _currentState.Exit();
        _currentState = nextState;
        nextState.Enter();
    }

    /// <summary>
    /// ���݂�State��Update�������s�����\�b�h
    /// </summary>
    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }
}
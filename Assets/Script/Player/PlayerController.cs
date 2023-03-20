using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly PlayerStateMachine _stateMachine;     

    void Start()
    {
        _stateMachine.Initialized(new PlayerIdleState(this));
    }

    void Update()
    {
        _stateMachine.Update();
    }
}

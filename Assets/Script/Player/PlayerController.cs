using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Player�̈ړ��Ɋւ��鏈�����`����N���X")]
    [SerializeField]
    private PlayerMove _mover;

    private readonly PlayerStateMachine _stateMachine = new();
    /// <summary>
    /// Player��State���Ǘ�����N���X
    /// </summary>
    public PlayerStateMachine StateMachine => _stateMachine;

    [Inject]
    private readonly IInputProvider _input;
    public IInputProvider Input => _input;

    void Start()
    {
        _mover.Initialize(gameObject.transform);

        _stateMachine.Initialized(new PlayerIdleState(this));
    }

    void Update()
    {
        _stateMachine.Update();
    }

    public void Walk()
    {
        _mover.Walk(_input.GetMoveDir());
    }
}

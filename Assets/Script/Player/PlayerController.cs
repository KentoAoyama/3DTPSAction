using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Player��Rigidbody")]
    [SerializeField]
    private Rigidbody _rb;

    [Tooltip("Player�̈ړ��Ɋւ��鏈�����`����N���X")]
    [SerializeField]
    private PlayerMove _move;

    [Tooltip("Player�̃_�b�V���Ɋւ��鏈�����`����N���X")]
    [SerializeField]
    private PlayerDash _dash;

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
        _move.Initialize(gameObject.transform);
        _dash.Initialize(gameObject.transform, _rb);
        _stateMachine.Initialized(new PlayerIdleState(this));
    }

    void Update()
    {
        _stateMachine.Update();
    }

    /// <summary>
    /// �ʏ�̈ړ����ɍs������
    /// </summary>
    public void Walk()
    {
        _move.Walk(_input.GetMoveDir());
    }

    /// <summary>
    /// Dash�s���ۂɍs������
    /// </summary>
    public void Dash()
    {
        _dash.Dash(_input.GetMoveDir());
    }
}

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.VFX;
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
        Initialized();
    }

    /// <summary>
    /// �N���X�̏������������s��
    /// </summary>
    private void Initialized()
    {
        _move.Initialized(gameObject.transform, _rb);
        _dash.Initialized(gameObject.transform, _rb);
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
        _move.Move(_input.GetMoveDir());
    }

    public void Dash()
    {
        _move.Move(_input.GetMoveDir(), _dash.DashSpeed);
    }

    /// <summary>
    /// Dash�s���ۂ̏���
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        await _dash.DashAsync(token);
    }
}

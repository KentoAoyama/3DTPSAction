using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using VContainer;
using static UnityEngine.UI.Image;

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

    [Tooltip("Player�̗������Ɋւ��鏈�����`����N���X")]
    [SerializeField]
    private PlayerFall _fall;

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
        _fall.Initialized(gameObject.transform);
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

    /// <summary>
    /// Dash���ɍs���ړ�����
    /// </summary>
    public void Dash()
    {
        _dash.DashMove(_input.GetMoveDir());
    }

    /// <summary>
    /// Dash���ɍs���ړ��ȊO�̏���
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        await _dash.DashAsync(token, this);
    }

�@�@public bool IsGround()
    {
        return _fall.IsGround();
    }

    public void FallMove()
    {
        _fall.FallMove();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position;
        origin.y = transform.position.y + 1f;
        Vector3 direction = -transform.up;
        Gizmos.DrawRay(origin, direction * _fall.RayLength);
    }
}

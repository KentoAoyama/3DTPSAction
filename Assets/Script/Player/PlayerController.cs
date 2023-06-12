using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using UnityEngine.Windows;
using VContainer;
using static UnityEditor.Experimental.GraphView.GraphView;
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

    [Tooltip("Player��Animation�Ɋւ��鏈�����`����N���X")]
    [SerializeField]
    private PlayerAnimation _animation;

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
        _move.Initialized();
        _dash.Initialized();
        _fall.Initialized();
        _stateMachine.Initialized(new PlayerIdleState(this));
    }

    void Update()
    {
        _animation.Update(_move.CurrentMoveSpeed);
        _stateMachine.Update();
    }

    /// <summary>
    /// �ʏ�̈ړ����ɍs������
    /// </summary>
    public void Walk()
    {
        _move.Move(
            gameObject.transform,
            _rb, 
            _input.GetMoveDir());
    }

    /// <summary>
    /// Dash���ɍs���ړ�����
    /// </summary>
    public void Dash()
    {
        _dash.DashMove(
            gameObject.transform,
            _rb, 
            _input.GetMoveDir());
    }

    public void Jump()
    {

    }

    /// <summary>
    /// Dash���ɍs���ړ��ȊO�̏���
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        await _dash.DashAsync( token, _input);
    }

�@�@public bool IsGround()
    {
        return _fall.IsGround(gameObject.transform);
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

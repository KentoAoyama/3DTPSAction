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
    [Tooltip("PlayerのRigidbody")]
    [SerializeField]
    private Rigidbody _rb;

    [Tooltip("Playerの移動に関する処理を定義するクラス")]
    [SerializeField]
    private PlayerMove _move;

    [Tooltip("Playerのダッシュに関する処理を定義するクラス")]
    [SerializeField]
    private PlayerDash _dash;

    [Tooltip("Playerの落下時に関する処理を定義するクラス")]
    [SerializeField]
    private PlayerFall _fall;

    private readonly PlayerStateMachine _stateMachine = new();
    /// <summary>
    /// PlayerのStateを管理するクラス
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
    /// クラスの初期化処理を行う
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
    /// 通常の移動時に行う処理
    /// </summary>
    public void Walk()
    {
        _move.Move(_input.GetMoveDir());
    }

    /// <summary>
    /// Dash時に行う移動処理
    /// </summary>
    public void Dash()
    {
        _dash.DashMove(_input.GetMoveDir());
    }

    /// <summary>
    /// Dash時に行う移動以外の処理
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        await _dash.DashAsync(token, this);
    }

　　public bool IsGround()
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

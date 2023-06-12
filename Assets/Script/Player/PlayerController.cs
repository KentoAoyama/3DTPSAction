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

    [Tooltip("PlayerのAnimationに関する処理を定義するクラス")]
    [SerializeField]
    private PlayerAnimation _animation;

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
    /// 通常の移動時に行う処理
    /// </summary>
    public void Walk()
    {
        _move.Move(
            gameObject.transform,
            _rb, 
            _input.GetMoveDir());
    }

    /// <summary>
    /// Dash時に行う移動処理
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
    /// Dash時に行う移動以外の処理
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        await _dash.DashAsync( token, _input);
    }

　　public bool IsGround()
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

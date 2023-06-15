using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    [Tooltip("Playerのジャンプに関する処理を定義するクラス")]
    [SerializeField]
    private PlayerJump _jump;

    [Tooltip("Playerの落下時に関する処理を定義するクラス")]
    [SerializeField]
    private PlayerFall _fall;

    [Tooltip("Playerの攻撃に関する処理を定義するクラス")]
    [SerializeField]
    private PlayerAttack _attack;

    [Tooltip("PlayerのAnimationに関する処理を定義するクラス")]
    [SerializeField]
    private PlayerAnimation _animation;
    /// <summary>
    /// PlayerのAnimationを管理するクラスのプロパティ。StateMachineで使う用
    /// </summary>
    public PlayerAnimation Animation => _animation;

    [SerializeField]
    private PlayerStateMachine _stateMachine = new();
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
    /// 腕のIKを設定
    /// </summary>
    private void OnAnimatorIK(int layerIndex)
    {
        _animation.SetIK(_attack.ShootPos);
    }

    /// <summary>
    /// 通常の移動時に行う処理
    /// </summary>
    public void Move()
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

    /// <summary>
    /// ジャンプの処理を行う
    /// </summary>
    public void Jump()
    {
        _jump.Jump(_rb);
    }

    /// <summary>
    /// 状態の遷移用。ジャンプの上昇処理を実行中かを表す
    /// </summary>
    /// <returns>ジャンプ中か</returns>
    public bool IsJump()
    {
        return _jump.IsJump();
    }

    /// <summary>
    /// Dash時に行う移動以外の処理
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        await _dash.DashAsync( token, _input);
    }

    /// <summary>
    /// 落下に関する処理
    /// </summary>
    public void Fall()
    {
        _fall.Fall(_rb);
    }

    /// <summary>
    /// 接地判定
    /// </summary>
    /// <returns>接地しているか</returns>
　　public bool IsGround()
    {
        bool isGround = _fall.IsGround(gameObject.transform);
        _animation.IsGroundParameterSet(isGround);

        return isGround;
    }

    public void Attack()
    {
        _attack.BulletShoot(
            _input.GetAim(),
            _input.GetShoot(),
            transform.position);

        _attack.ShootPositionSet();

        _animation.ChangeIKWeight(_input.GetAim());
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

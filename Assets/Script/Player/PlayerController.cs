using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

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
        _move.Initialize(gameObject.transform);
        _dash.Initialize(gameObject.transform, _rb);
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
        _move.Walk(_input.GetMoveDir());
    }

    /// <summary>
    /// Dash行う際に行う処理
    /// </summary>
    public void Dash()
    {
        _dash.Dash(_input.GetMoveDir());
    }
}

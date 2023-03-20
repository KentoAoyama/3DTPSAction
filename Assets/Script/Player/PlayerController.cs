using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Playerの移動に関する処理を定義するクラス")]
    [SerializeField]
    private PlayerMove _mover;

    private readonly PlayerStateMachine _stateMachine = new();
    /// <summary>
    /// PlayerのStateを管理するクラス
    /// </summary>
    public PlayerStateMachine StateMachine => _stateMachine;

    private float _h;
    private float _v;
    public float InputH => _h;
    public float InputV => _v;

    void Start()
    {
        _mover.Initialize(gameObject.transform);

        _stateMachine.Initialized(new PlayerIdleState(this));
    }

    void Update()
    {
        //とりあえず仮置きで入力を受け取る
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _stateMachine.Update();
    }

    public void Walk()
    {
        _mover.Walk(_h, _v);
    }
}

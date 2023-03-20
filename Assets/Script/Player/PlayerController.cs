using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //�Ƃ肠�������u���œ��͂��󂯎��
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _stateMachine.Update();
    }

    public void Walk()
    {
        _mover.Walk(_h, _v);
    }
}

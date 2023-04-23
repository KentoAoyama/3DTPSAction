using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDash
{
    [Tooltip("Player��Renderer")]
    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    [Tooltip("�ړ����鑬�x")]
    [SerializeField]
    private float _dashSpeed = 200f;

    [Tooltip("�ړ����鎞��")]
    [SerializeField]
    private float _dashTime = 1.5f;

    /// <summary>
    /// Player��Transform
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Player��Rigidbody
    /// </summary>
    private Rigidbody _rb;

    public void Initialize(Transform transform, Rigidbody rb)
    {
        _transform = transform;
        _rb = rb;
    }

    /// <summary>
    /// Player�̕��s�Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    public void Dash(Vector2 moveDir)
    {
        _renderer.enabled = false;
        Debug.Log("Dash!!!");
    }
}

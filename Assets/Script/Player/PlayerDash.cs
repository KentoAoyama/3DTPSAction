using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Text;
using System;

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

    public void Initialized(Transform transform, Rigidbody rb)
    {
        _transform = transform;
        _rb = rb;
    }

    /// <summary>
    /// Player�̕��s�Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        _renderer.enabled = false;
        Debug.Log("DashStart");

        await UniTask.Delay(TimeSpan.FromSeconds(_dashTime));

        _renderer.enabled = true;
        Debug.Log("DashFinish");
    }
}

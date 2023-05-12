using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Text;
using System;
using UnityEngine.VFX;

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

    [Tooltip("VisualEffect")]
    [SerializeField]
    private VisualEffect _effect;

    [Tooltip("Dash���̃G�t�F�N�g�̗�")]
    [SerializeField]
    private int _effectRate = 2000;

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
        _effect.SetInt("Rate", 0);
    }

    /// <summary>
    /// Player�̕��s�Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    public async UniTask DashAsync(CancellationToken token)
    {
        _effect.SetInt("Rate", _effectRate);
        _renderer.enabled = false;
        Debug.Log("DashStart");

        await UniTask.Delay(TimeSpan.FromSeconds(_dashTime));

        _effect.SetInt("Rate", 0);
        _renderer.enabled = true;
        Debug.Log("DashFinish");
    }
}

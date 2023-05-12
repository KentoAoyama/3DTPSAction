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
    [Tooltip("PlayerのRenderer")]
    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    [Tooltip("移動する速度")]
    [SerializeField]
    private float _dashSpeed = 200f;

    [Tooltip("移動する時間")]
    [SerializeField]
    private float _dashTime = 1.5f;

    [Tooltip("VisualEffect")]
    [SerializeField]
    private VisualEffect _effect;

    [Tooltip("Dash時のエフェクトの量")]
    [SerializeField]
    private int _effectRate = 2000;

    /// <summary>
    /// PlayerのTransform
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// PlayerのRigidbody
    /// </summary>
    private Rigidbody _rb;

    public void Initialized(Transform transform, Rigidbody rb)
    {
        _transform = transform;
        _rb = rb;
        _effect.SetInt("Rate", 0);
    }

    /// <summary>
    /// Playerの歩行に関する処理を定義するメソッド
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// PlayerのTransform
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// PlayerのRigidbody
    /// </summary>
    private Rigidbody _rb;

    public void Initialize(Transform transform, Rigidbody rb)
    {
        _transform = transform;
        _rb = rb;
    }

    /// <summary>
    /// Playerの歩行に関する処理を定義するメソッド
    /// </summary>
    public void Dash(Vector2 moveDir)
    {
        _renderer.enabled = false;
        Debug.Log("Dash!!!");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx.Triggers;
using UnityEngine;

[System.Serializable]
public class PlayerJump
{
    [Tooltip("ジャンプのスピード")]
    [SerializeField]
    private float _jumpSpeed = 200f;

    [Tooltip("上昇の加速度")]
    [SerializeField]
    private float _upAcceleration = 1f;

    [Tooltip("ジャンプするまでにかかる時間")]
    [SerializeField]
    private float _jumpInterval = 0.5f;

    private float _currentUpSpeed = 1f;

    private float _timer = 0f;

    [SerializeField]
    private bool _isJump;

    public void Initilaized()
    {
        _currentUpSpeed = 1f;
        _timer = 0f;
    }

    /// <summary>
    /// ジャンプの処理を行うメソッド
    /// </summary>
    /// <returns>ジャンプの動作が終了したかどうかを返す</returns>
    public void Jump(Rigidbody rb)
    {
        var deltaTime = Time.deltaTime;

        _timer += deltaTime;
        if (_timer < _jumpInterval) return;

        //ジャンプした瞬間のvelocity
        Vector3 xyVelocity = new(rb.velocity.x, 0f, rb.velocity.z);
        var velocity = _jumpSpeed * Vector3.up + xyVelocity;

        //速度を線形補完する
        velocity = Vector3.Lerp(xyVelocity, velocity, _currentUpSpeed);

        //現在の減速度を計算
        _currentUpSpeed -= deltaTime / _upAcceleration;
        _currentUpSpeed = Mathf.Clamp01(_currentUpSpeed);

        rb.velocity = velocity;
    }

    public bool IsJump()
    {
        if (_currentUpSpeed <= 0.01)
        {
            _currentUpSpeed = 1f;
            _timer = 0f;

            _isJump = false;

            return false;
        }
        else
        {
            _isJump = true;

            return true;
        }
    }
}

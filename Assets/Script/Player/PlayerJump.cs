using System.Collections;
using System.Collections.Generic;
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

    [Tooltip("ジャンプ中の移動速度の最低値")]
    [SerializeField, Range(0, 1)]
    private float _speedDownLimit = 0.1f;

    private float _currentUpSpeed = 1f;

    public void Initilaized()
    {
        _currentUpSpeed = 1f;
    }

    /// <summary>
    /// ジャンプの処理を行うメソッド
    /// </summary>
    /// <returns>ジャンプの動作が終了したかどうかを返す</returns>
    public void Jump(Rigidbody rb)
    {
        var deltaTime = Time.deltaTime;

        Vector3 moveVec = new(rb.velocity.x, 0f, rb.velocity.z);
        var velocity = _jumpSpeed * Vector3.up + moveVec * _speedDownLimit;

        _currentUpSpeed -= deltaTime / _upAcceleration;
        _currentUpSpeed = Mathf.Clamp01(_currentUpSpeed);
        var xyVelocity = rb.velocity;
        xyVelocity.y = 0f;
        velocity = Vector3.Slerp(xyVelocity, velocity, _currentUpSpeed);

        rb.velocity = velocity;
    }

    public bool IsJump()
    {
        if (_currentUpSpeed <= 0.01)
        {
            _currentUpSpeed = 1f;
            return false;
        }
        else
        {
            return true;
        }
    }
}

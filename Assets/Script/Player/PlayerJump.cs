using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx.Triggers;
using UnityEngine;

[System.Serializable]
public class PlayerJump
{
    [Tooltip("�W�����v�̃X�s�[�h")]
    [SerializeField]
    private float _jumpSpeed = 200f;

    [Tooltip("�㏸�̉����x")]
    [SerializeField]
    private float _upAcceleration = 1f;

    [Tooltip("�W�����v����܂łɂ����鎞��")]
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
    /// �W�����v�̏������s�����\�b�h
    /// </summary>
    /// <returns>�W�����v�̓��삪�I���������ǂ�����Ԃ�</returns>
    public void Jump(Rigidbody rb)
    {
        var deltaTime = Time.deltaTime;

        _timer += deltaTime;
        if (_timer < _jumpInterval) return;

        //�W�����v�����u�Ԃ�velocity
        Vector3 xyVelocity = new(rb.velocity.x, 0f, rb.velocity.z);
        var velocity = _jumpSpeed * Vector3.up + xyVelocity;

        //���x����`�⊮����
        velocity = Vector3.Lerp(xyVelocity, velocity, _currentUpSpeed);

        //���݂̌����x���v�Z
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

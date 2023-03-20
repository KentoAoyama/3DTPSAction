using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("�ړ����鑬�x")]
    [SerializeField]
    private float _walkSpeed = 200f;

    [Tooltip("������ύX����X�s�[�h")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("Player��RigidBody")]
    [SerializeField]
    private Rigidbody _rb;

    /// <summary>
    /// Player��Transform
    /// </summary>
    private Transform _transform;

    public void Initialize(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// Player�̕��s�Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    /// <param name="inputH">�����@�̓���</param>
    /// <param name="inputV">�c�����̓���</param>
    public void Walk(float inputH, float inputV)
    {
        var deltaTime = Time.deltaTime;

        //�ړ�������������J�����̌������Q�Ƃ������̂ɂ���
        var velocity = Vector3.right * inputH + Vector3.forward * inputV;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y = 0;

        //�ړ����s������
        _rb.velocity = _walkSpeed * deltaTime * velocity.normalized;

        //���������X�ɕύX����
        Quaternion changeRotation = default;      
        if (velocity.sqrMagnitude > 0.5f)�@//���x�����ȏ�Ȃ�A������ύX����
        {
            changeRotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�������
        _transform.rotation = 
            Quaternion.RotateTowards(
                _transform.rotation, 
                changeRotation, 
                _rotationSpeed * deltaTime);
    }
}

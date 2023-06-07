using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("������ύX����X�s�[�h")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    /// <summary>
    /// Player��Transform
    /// </summary>
    private Transform _transform;

    private Rigidbody _rb;

    public void Initialized(Transform transform, Rigidbody rb)
    {
        _transform = transform;
        _rb = rb; ;
    }

    /// <summary>
    /// Player�̈ړ��Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>

    public void Move(Vector2 moveDir, float speed = 200f)
    {
        var deltaTime = Time.deltaTime;

        //�ړ�������������J�����̌������Q�Ƃ������̂ɂ���
        var velocity = Vector3.right * moveDir.x + Vector3.forward * moveDir.y;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity = speed * deltaTime * velocity.normalized;
        velocity.y = _rb.velocity.y;

        //�ړ����s������
        _rb.velocity = velocity;

        //���������X�ɕύX����
        Quaternion changeRotation = default;
        if (velocity.sqrMagnitude > 0.5f)�@//���x�����ȏ�Ȃ�A������ύX����
        {
            velocity.y = 0f;
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

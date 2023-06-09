using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("�ړ�����X�s�[�h")]
    [SerializeField]
    private float _speed = 500f;

    [Tooltip("������ύX����X�s�[�h")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("�ړ��̉����x")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("�ړ��̌����x")]
    [SerializeField]
    private float _stopAcceleration = 1f;

    [SerializeField]
    private float _moveTimer = 0f;
    [SerializeField]
    private float _stopTimer = 0f;

    private Vector3 _currentVeclocity;

    public void Initialized()
    {
        _moveTimer = 0;
        _stopTimer = 0;
    }

    /// <summary>
    /// Player�̈ړ��Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    public void Move(Transform moveTransform, Rigidbody rb, Vector2 moveDir)
    {
        var deltaTime = Time.deltaTime;

        //���͂����邩�ǂ����m�F
        if (moveDir != Vector2.zero)
        {
            //��~���Ɏg�p����^�C�}�[�����Z�b�g
            _stopTimer = 0f;

            //�ړ�������������J�����̌������Q�Ƃ������̂ɂ���
            var velocity = Vector3.right * moveDir.x + Vector3.forward * moveDir.y;
            velocity = Camera.main.transform.TransformDirection(velocity).normalized;
            velocity = _speed * deltaTime * velocity;
            velocity.y = rb.velocity.y;

            //�ړ��̑��x�����ʐ��`��Ԃ���
            _moveTimer += deltaTime;
            float t = Mathf.Clamp01(_moveTimer / _moveAcceleration); //0����1�͈̔͂ɃN�����v
            velocity = Vector3.Slerp(Vector3.zero, velocity, t);

            //�ړ����s������
            rb.velocity = velocity;

            //���������X�ɕύX����
            Quaternion changeRotation = default;
            if (velocity.sqrMagnitude > 0.5f) //���x�����ȏ�Ȃ�A������ύX����
            {
                velocity.y = 0f;
                changeRotation = Quaternion.LookRotation(velocity, Vector3.up);
            }
            //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�������
            moveTransform.rotation =
                Quaternion.RotateTowards(
                    moveTransform.rotation,
                    changeRotation,
                    _rotationSpeed * deltaTime);

            _currentVeclocity = velocity;
        }
        else
        {
            //�ړ����Ɏg�p����^�C�}�[�����Z�b�g
            _moveTimer = 0f;

            _stopTimer += deltaTime;
            float t = Mathf.Clamp01(_stopTimer / _stopAcceleration); //0����1�͈̔͂ɃN�����v
            rb.velocity = Vector3.Slerp(_currentVeclocity, Vector3.zero, t);
        }
    }
}

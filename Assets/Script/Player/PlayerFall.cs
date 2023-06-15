using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerFall
{
    [Tooltip("Ray�̒���")]
    [SerializeField]
    private float _rayLength = 10f;
    public float RayLength => _rayLength;

    [Tooltip("�ڒn������Ƃ�Layer")]
    [SerializeField]
    private LayerMask _layer;

    [Tooltip("�����̃X�s�[�h")]
    [SerializeField]
    private float _fallSpeed = 200f;

    [Tooltip("�����̉����x")]
    [SerializeField]
    private float _downAcceleration = 1f;

    private float _currentDownSpeed = 0f;

    [SerializeField]
    private bool _isGround;


    public void Initialized()
    {
        _currentDownSpeed = 0f;
    }

    public void Fall(Rigidbody rb)
    {
        
    }

    /// <summary>
    /// Raycast��p���Đڒn������s��
    /// </summary>
    /// <param name="origin">Ray�̎n���_</param>
    /// <param name="direction">Ray��������</param>
    /// <returns>�ڒn���Ă��邩</returns>
    public bool IsGround(Transform transform)
    {
        Vector3 origin = transform.position;
        origin.y = transform.position.y + 1f;
        Vector3 direction = -transform.up;

        Ray ray = new (origin, direction);
        bool isGround = Physics.Raycast(ray, _rayLength, _layer);

        if (isGround)
        {
            _currentDownSpeed = 0;
        }

        _isGround = isGround;

        return isGround;
    }
}

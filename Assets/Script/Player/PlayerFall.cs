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

    public void Initialized()
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
        return isGround;
    }

    /// <summary>
    /// �������̓���
    /// </summary>
    public void FallMove()
    {

    }
}

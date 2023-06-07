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

    private Transform _transform;

    public void Initialized(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// Raycast��p���Đڒn������s��
    /// </summary>
    /// <param name="origin">Ray�̎n���_</param>
    /// <param name="direction">Ray��������</param>
    /// <returns>�ڒn���Ă��邩</returns>
    public bool IsGround()
    {
        Vector3 origin = _transform.position;
        origin.y = _transform.position.y + 1f;
        Vector3 direction = -_transform.up;

        Ray ray = new (origin, direction);
        bool isGround = Physics.Raycast(ray, _rayLength, _layer);
        return isGround;
    }

    public void FallMove()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerFall
{
    [Tooltip("Rayの長さ")]
    [SerializeField]
    private float _rayLength = 10f;

    public float RayLength => _rayLength;

    [Tooltip("接地判定をとるLayer")]
    [SerializeField]
    private LayerMask _layer;

    private Transform _transform;

    public void Initialized(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// Raycastを用いて接地判定を行う
    /// </summary>
    /// <param name="origin">Rayの始発点</param>
    /// <param name="direction">Rayを撃つ方向</param>
    /// <returns>接地しているか</returns>
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

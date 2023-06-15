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

    [Tooltip("落下のスピード")]
    [SerializeField]
    private float _fallSpeed = 200f;

    [Tooltip("落下の加速度")]
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
    /// Raycastを用いて接地判定を行う
    /// </summary>
    /// <param name="origin">Rayの始発点</param>
    /// <param name="direction">Rayを撃つ方向</param>
    /// <returns>接地しているか</returns>
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

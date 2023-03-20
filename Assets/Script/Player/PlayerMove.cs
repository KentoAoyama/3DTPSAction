using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("移動する速度")]
    [SerializeField]
    private float _walkSpeed = 200f;

    [Tooltip("向きを変更するスピード")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("PlayerのRigidBody")]
    [SerializeField]
    private Rigidbody _rb;

    /// <summary>
    /// PlayerのTransform
    /// </summary>
    private Transform _transform;

    public void Initialize(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// Playerの歩行に関する処理を定義するメソッド
    /// </summary>
    /// <param name="inputH">横方法の入力</param>
    /// <param name="inputV">縦方向の入力</param>
    public void Walk(float inputH, float inputV)
    {
        var deltaTime = Time.deltaTime;

        //移動をする方向をカメラの向きを参照したものにする
        var velocity = Vector3.right * inputH + Vector3.forward * inputV;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y = 0;

        //移動を行う処理
        _rb.velocity = _walkSpeed * deltaTime * velocity.normalized;

        //向きを徐々に変更する
        Quaternion changeRotation = default;      
        if (velocity.sqrMagnitude > 0.5f)　//速度が一定以上なら、向きを変更する
        {
            changeRotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        //第1引数のQuaternionを第２引数のQuaternionまで第３引数の速度で変化させる
        _transform.rotation = 
            Quaternion.RotateTowards(
                _transform.rotation, 
                changeRotation, 
                _rotationSpeed * deltaTime);
    }
}

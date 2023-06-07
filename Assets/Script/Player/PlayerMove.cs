using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("向きを変更するスピード")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    /// <summary>
    /// PlayerのTransform
    /// </summary>
    private Transform _transform;

    private Rigidbody _rb;

    public void Initialized(Transform transform, Rigidbody rb)
    {
        _transform = transform;
        _rb = rb; ;
    }

    /// <summary>
    /// Playerの移動に関する処理を定義するメソッド
    /// </summary>

    public void Move(Vector2 moveDir, float speed = 200f)
    {
        var deltaTime = Time.deltaTime;

        //移動をする方向をカメラの向きを参照したものにする
        var velocity = Vector3.right * moveDir.x + Vector3.forward * moveDir.y;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity = speed * deltaTime * velocity.normalized;
        velocity.y = _rb.velocity.y;

        //移動を行う処理
        _rb.velocity = velocity;

        //向きを徐々に変更する
        Quaternion changeRotation = default;
        if (velocity.sqrMagnitude > 0.5f)　//速度が一定以上なら、向きを変更する
        {
            velocity.y = 0f;
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

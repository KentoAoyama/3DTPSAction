using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("移動するスピード")]
    [SerializeField]
    private float _speed = 500f;

    [Tooltip("向きを変更するスピード")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("移動の加速度")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("移動の減速度")]
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
    /// Playerの移動に関する処理を定義するメソッド
    /// </summary>
    public void Move(Transform moveTransform, Rigidbody rb, Vector2 moveDir)
    {
        var deltaTime = Time.deltaTime;

        //入力があるかどうか確認
        if (moveDir != Vector2.zero)
        {
            //停止時に使用するタイマーをリセット
            _stopTimer = 0f;

            //移動をする方向をカメラの向きを参照したものにする
            var velocity = Vector3.right * moveDir.x + Vector3.forward * moveDir.y;
            velocity = Camera.main.transform.TransformDirection(velocity).normalized;
            velocity = _speed * deltaTime * velocity;
            velocity.y = rb.velocity.y;

            //移動の速度を球面線形補間する
            _moveTimer += deltaTime;
            float t = Mathf.Clamp01(_moveTimer / _moveAcceleration); //0から1の範囲にクランプ
            velocity = Vector3.Slerp(Vector3.zero, velocity, t);

            //移動を行う処理
            rb.velocity = velocity;

            //向きを徐々に変更する
            Quaternion changeRotation = default;
            if (velocity.sqrMagnitude > 0.5f) //速度が一定以上なら、向きを変更する
            {
                velocity.y = 0f;
                changeRotation = Quaternion.LookRotation(velocity, Vector3.up);
            }
            //第1引数のQuaternionを第２引数のQuaternionまで第３引数の速度で変化させる
            moveTransform.rotation =
                Quaternion.RotateTowards(
                    moveTransform.rotation,
                    changeRotation,
                    _rotationSpeed * deltaTime);

            _currentVeclocity = velocity;
        }
        else
        {
            //移動時に使用するタイマーをリセット
            _moveTimer = 0f;

            _stopTimer += deltaTime;
            float t = Mathf.Clamp01(_stopTimer / _stopAcceleration); //0から1の範囲にクランプ
            rb.velocity = Vector3.Slerp(_currentVeclocity, Vector3.zero, t);
        }
    }
}

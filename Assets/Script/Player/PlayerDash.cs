using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class PlayerDash
{
    [Tooltip("PlayerのRenderer")]
    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    [Tooltip("移動する速度")]
    [SerializeField]
    private float _dashSpeed = 200f;

    [Tooltip("旋回する速度")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("VisualEffect")]
    [SerializeField]
    private VisualEffect _effect;

    [Tooltip("Dash時のエフェクトの量")]
    [SerializeField]
    private int _effectRate = 2000;

    /// <summary>
    /// PlayerのTransform
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// PlayerのRigidbody
    /// </summary>
    private Rigidbody _rb;

    public void Initialized(Transform transform, Rigidbody rb)
    {
        _transform = transform;
        _rb = rb;
        _effect.SetInt("Rate", 0);
    }

    /// <summary>
    /// Playerの歩行に関する処理を定義するメソッド
    /// </summary>
    public async UniTask DashAsync(CancellationToken token, PlayerController player)
    {
        _effect.SetInt("Rate", _effectRate);
        _renderer.enabled = false;

        await UniTask.WaitWhile(() => player.Input.GetDash(), cancellationToken: token);

        _effect.SetInt("Rate", 0);
        _renderer.enabled = true;
    }

    /// <summary>
    /// ダッシュをしているときの移動処理
    /// Y方向の動きがある以外はほとんど同じ
    /// </summary>
    /// <param name="moveDir"></param>
    public void DashMove(Vector2 moveDir)
    {
        var deltaTime = Time.deltaTime;

        //移動をする方向をカメラの向きを参照したものにする
        var velocity = Vector3.right * moveDir.x + Vector3.forward * moveDir.y;
        velocity = Camera.main.transform.TransformDirection(velocity);

        //移動を行う処理
        _rb.velocity = _dashSpeed * deltaTime * velocity.normalized;

        //向きを徐々に変更する
        Quaternion changeRotation = default;
        velocity.y = 0f;
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

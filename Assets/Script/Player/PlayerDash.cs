using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class PlayerDash
{
    [Tooltip("Player��Renderer")]
    [SerializeField]
    private SkinnedMeshRenderer _renderer;

    [Tooltip("�ړ����鑬�x")]
    [SerializeField]
    private float _dashSpeed = 200f;

    [Tooltip("���񂷂鑬�x")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("VisualEffect")]
    [SerializeField]
    private VisualEffect _effect;

    [Tooltip("Dash���̃G�t�F�N�g�̗�")]
    [SerializeField]
    private int _effectRate = 2000;

    private const string EFFECT_RATE = "Rate";

    public void Initialized()
    {
        _effect.SetInt(EFFECT_RATE, 0);
    }

    /// <summary>
    /// Player�̕��s�Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    public async UniTask DashAsync(CancellationToken token, IInputProvider input)
    {
        _effect.SetInt(EFFECT_RATE, _effectRate);
        _renderer.enabled = false;

        await UniTask.WaitWhile(() => input.GetDash(), cancellationToken: token);

        _effect.SetInt(EFFECT_RATE, 0);
        _renderer.enabled = true;
    }

    /// <summary>
    /// �_�b�V�������Ă���Ƃ��̈ړ�����
    /// Y�����̓���������ȊO�͂قƂ�Ǔ���
    /// </summary>
    /// <param name="moveDir"></param>
    public void DashMove(Transform moveTransform, Rigidbody rb, Vector2 moveDir)
    {
        var deltaTime = Time.deltaTime;

        //�ړ�������������J�����̌������Q�Ƃ������̂ɂ���
        var velocity = Vector3.right * moveDir.x + Vector3.forward * moveDir.y;
        velocity = Camera.main.transform.TransformDirection(velocity);

        //�ړ����s������
        rb.velocity = _dashSpeed * deltaTime * velocity.normalized;

        //���������X�ɕύX����
        Quaternion changeRotation = default;
        velocity.y = 0f;
        if (velocity.sqrMagnitude > 0.5f)�@//���x�����ȏ�Ȃ�A������ύX����
        {
            changeRotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�������
        moveTransform.rotation =
            Quaternion.RotateTowards(
                moveTransform.rotation,
                changeRotation,
                _rotationSpeed * deltaTime);
    }
}

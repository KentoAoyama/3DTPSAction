using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [Tooltip("PlayerのAnimator")]
    [SerializeField]
    private Animator _animator;

    /// <summary>
    /// PlayerController内のUpdateで行う処理
    /// </summary>
    public void Update(float speed)
    {
        WalkParameterSet(speed);
    }

    /// <summary>
    /// 移動時のアニメーションを受け取る
    /// </summary>
    private void WalkParameterSet(float speed)
    {
        _animator.SetFloat(
            "Speed",
            speed);
    }

    public void JumpParameterSet(bool isJump)
    {
        _animator.SetBool(
            "IsJump",
            isJump);
    }

    public void IsGroundParameterSet(bool isGround)
    {
        _animator.SetBool(
            "IsGround",
            isGround);
    }
}

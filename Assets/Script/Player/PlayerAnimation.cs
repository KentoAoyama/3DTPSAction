using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [Tooltip("Player��Animator")]
    [SerializeField]
    private Animator _animator;

    /// <summary>
    /// PlayerController����Update�ōs������
    /// </summary>
    public void Update(float speed)
    {
        WalkParameterSet(speed);
    }

    /// <summary>
    /// �ړ����̃A�j���[�V�������󂯎��
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

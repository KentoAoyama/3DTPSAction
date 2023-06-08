using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [Tooltip("Player��Animator")]
    [SerializeField]
    private Animator _animator;

    private PlayerController _player;

    public void Initialized(PlayerController player)
    {
        _player = player;
    }

    /// <summary>
    /// PlayerController����Update�ōs������
    /// </summary>
    public void Update()
    {
        WalkParameterSet();
    }

    /// <summary>
    /// �ړ����̃A�j���[�V�������󂯎��
    /// </summary>
    private void WalkParameterSet()
    {
        _animator.SetFloat(
            "InputMove",
            _player.Input.GetMoveDir().sqrMagnitude);
    }
}

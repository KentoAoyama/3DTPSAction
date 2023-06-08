using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [Tooltip("PlayerのAnimator")]
    [SerializeField]
    private Animator _animator;

    private PlayerController _player;

    public void Initialized(PlayerController player)
    {
        _player = player;
    }

    /// <summary>
    /// PlayerController内のUpdateで行う処理
    /// </summary>
    public void Update()
    {
        WalkParameterSet();
    }

    /// <summary>
    /// 移動時のアニメーションを受け取る
    /// </summary>
    private void WalkParameterSet()
    {
        _animator.SetFloat(
            "InputMove",
            _player.Input.GetMoveDir().sqrMagnitude);
    }
}

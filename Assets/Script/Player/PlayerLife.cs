using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InGameController;

[System.Serializable]
public class PlayerLife
{
    [Tooltip("Player�̍ő僉�C�t")]
    [SerializeField]
    private int _lifePoint = 5;

    [Tooltip("GUI���Ǘ�����N���X")]
    [SerializeField]
    private GUIController _gui;

    [Header("�f�o�b�O�p")]
    [SerializeField]
    private int _currentMaxLife = 0;

    private int _currentLifePoint = 0;

    public void Initilaized(int skillValue)
    {
        _currentLifePoint = _lifePoint;
        _gui.LifePointText.text = (_currentLifePoint + skillValue).ToString();
        _currentMaxLife = _currentLifePoint + skillValue;
    }

    public void Hit(int damage, int skillValue)
    {
        _currentLifePoint -= damage;

        _currentMaxLife = _currentLifePoint + skillValue;

        if (_currentMaxLife <= 1)
        {
            _gui.GameOverPanel.SetActive(true);
        }

        _gui.LifePointText.text = _currentMaxLife.ToString();
    }
}

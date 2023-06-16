using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InGameController;

[System.Serializable]
public class PlayerLife
{
    [Tooltip("Playerの最大ライフ")]
    [SerializeField]
    private int _lifePoint = 5;

    [Tooltip("GUIを管理するクラス")]
    [SerializeField]
    private GUIController _gui;

    [Header("デバッグ用")]
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

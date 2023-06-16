using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _skillTreeUI;

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private GUIController _gui;


    public enum GameState
    {
        WaitStart,
        InGame,
        OpenWindow
    }

    [SerializeField]//デバッグ用
    private GameState _state = GameState.InGame;
    public GameState State => _state;

    private IInputProvider _input;

    /// <summary>
    /// Skillによって上昇するパラメーターの構造体
    /// </summary>
    public struct SkillValue
    {
        public int AttackDamageUp;

        public float DashSpeedUp;

        public int LifePointUp;

        public float AttackIntervalDown;
    }

    private SkillValue _skillValue = new();

    private void Start()
    {
        _state = GameState.WaitStart;
        _input = _player.Input;

        ClosePanel();

        _player.Initialized();
    }

    private void Update()
    {
        if (_state == GameState.WaitStart) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gui.GameStart();
                _state = GameState.InGame;
            }
            return;
        }

        if (_input.GetChangeWindow())
        {
            if (_state == GameState.InGame)
            {
                _skillTreeUI.SetActive(true);
                _state = GameState.OpenWindow;
            }
            else if (_state == GameState.OpenWindow)
            {
                ClosePanel();
                _state = GameState.InGame;
            }
        }
    }

    public void ClosePanel()
    {
        if (_skillTreeUI != null) 
        {
            _skillTreeUI.SetActive(false);
            SkillValueSet();
            _player.SkillValue = _skillValue;
        }
    }

    private void SkillValueSet()
    {
        var skills = SkillDataBase.Instance;
        SkillValue skillValue = new();

        foreach (var skillSetNo in skills.SkillSetNo)
        {
            var skill = skills.SkillData[skillSetNo];

            skillValue.AttackDamageUp += skill.AttackDamageUp;
            skillValue.DashSpeedUp += skill.DashSpeedUp;
            skillValue.LifePointUp += skill.LifePointUp;
            skillValue.AttackIntervalDown += skill.AttackIntervalDown;
        }

        _skillValue = skillValue;
    }
}

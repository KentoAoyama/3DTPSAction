using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill System/Skill")]
public class SkillObject : ScriptableObject
{
    [Header("スキルの名前"), SerializeField]
    string _skillName;
    public string SkillName => _skillName;

    [Header("スキルの種類"), Tooltip("Skillの種類"),SerializeField]
    SkillTypeSample _type;
    public SkillTypeSample Type => _type;

    [Header("スキルの説明"),Tooltip("Skillの説明"),SerializeField]
    [TextArea(10, 10)]
    string _description;
    public string Description => _description;

    [Header("取得に必要なスキルポイント"), Tooltip("取得に必要なスキルポイント"), SerializeField]
    int _skillPoint = 1;
    public int SkillPoint => _skillPoint;

    [Header("攻撃のダメージアップ")]
    [SerializeField]
    private int _attackDamageUp = 0;
    public int AttackDamageUp => _attackDamageUp;

    [Header("Dash時の移動速度アップ")]
    [SerializeField]
    private float _dashSpeedUp = 0f;
    public float DashSpeedUp => _dashSpeedUp;

    [Header("ライフポイントアップ")]
    [SerializeField]
    private int _lifePointUp = 0;
    public int LifePointUp => _lifePointUp;

    [Header("攻撃のインターバル短縮")]
    [SerializeField, Range(0f, 1f)]
    private float _attackIntervalDown = 0f;
    public float AttackIntervalDown => _attackIntervalDown;
}

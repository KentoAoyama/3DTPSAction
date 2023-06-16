using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill System/Skill")]
public class SkillObject : ScriptableObject
{
    [Header("�X�L���̖��O"), SerializeField]
    string _skillName;
    public string SkillName => _skillName;

    [Header("�X�L���̎��"), Tooltip("Skill�̎��"),SerializeField]
    SkillTypeSample _type;
    public SkillTypeSample Type => _type;

    [Header("�X�L���̐���"),Tooltip("Skill�̐���"),SerializeField]
    [TextArea(10, 10)]
    string _description;
    public string Description => _description;

    [Header("�擾�ɕK�v�ȃX�L���|�C���g"), Tooltip("�擾�ɕK�v�ȃX�L���|�C���g"), SerializeField]
    int _skillPoint = 1;
    public int SkillPoint => _skillPoint;

    [Header("�U���̃_���[�W�A�b�v")]
    [SerializeField]
    private int _attackDamageUp = 0;
    public int AttackDamageUp => _attackDamageUp;

    [Header("Dash���̈ړ����x�A�b�v")]
    [SerializeField]
    private float _dashSpeedUp = 0f;
    public float DashSpeedUp => _dashSpeedUp;

    [Header("���C�t�|�C���g�A�b�v")]
    [SerializeField]
    private int _lifePointUp = 0;
    public int LifePointUp => _lifePointUp;

    [Header("�U���̃C���^�[�o���Z�k")]
    [SerializeField, Range(0f, 1f)]
    private float _attackIntervalDown = 0f;
    public float AttackIntervalDown => _attackIntervalDown;
}

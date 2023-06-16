using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataBase : SingletonMonoBehaviour<SkillDataBase>
{
    [Header("�X�L���f�[�^"),Tooltip("SkillData")]
    [SerializeField] SkillObject[] _skillData;
    public SkillObject[] SkillData => _skillData;

    [Header("�X�L���̎擾��"),Tooltip("�X�L���̎擾��")]
    public bool[] Skillbool;

    [Header("�X�L���̃Z�b�g\n(�X�L���f�[�^�̗v�f������Q��)"),Tooltip("�X�L���̃Z�b�g")]
    public int[] SkillSetNo;

    [Header("�������Ă���X�L���|�C���g"), Tooltip("�������Ă���X�L���|�C���g"),SerializeField]
    private int _skillPoint = 0;
    public int SkillPoint 
    { 
        get => _skillPoint; 
        set => _skillPoint = value; 
    }

    protected override bool _dontDestroyOnLoad { get { return true; } }

    public void GetSkillPoint(int i)
    {
        _skillPoint += i;
    }
}

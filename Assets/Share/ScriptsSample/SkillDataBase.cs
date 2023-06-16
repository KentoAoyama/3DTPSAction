using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataBase : SingletonMonoBehaviour<SkillDataBase>
{
    [Header("スキルデータ"),Tooltip("SkillData")]
    [SerializeField] SkillObject[] _skillData;
    public SkillObject[] SkillData => _skillData;

    [Header("スキルの取得状況"),Tooltip("スキルの取得状況")]
    public bool[] Skillbool;

    [Header("スキルのセット\n(スキルデータの要素数から参照)"),Tooltip("スキルのセット")]
    public int[] SkillSetNo;

    [Header("今持っているスキルポイント"), Tooltip("今持っているスキルポイント"),SerializeField]
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

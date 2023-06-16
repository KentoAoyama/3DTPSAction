using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ButtonsDataController;

public class SkillDataSave : MonoBehaviour
{
    private SkillDataBase _skillDataBase;

    /// <summary>
    /// 今回保存するファイルの名前
    /// </summary>
    private const string FILE_NAME = "SkillData";

    /// <summary>
    /// 今回保存するスキルのデータ
    /// </summary>
    [System.Serializable]
    public struct SkillData
    {
        public bool[] SkillBool;
        public int[] SkillSetNo;
        public int SkillPoint;
    }

    private void Awake()
    {
        _skillDataBase = FindObjectOfType<SkillDataBase>().GetComponent<SkillDataBase>();

        DataLoad();
    }

    private void DataLoad()
    {
        SkillData loadSkillData = DataSaveManager.Instance.Load<SkillData>(FILE_NAME);

        //データを読み込めなかった場合
        if (loadSkillData.SkillBool == null || loadSkillData.SkillSetNo == null ||
            loadSkillData.SkillBool.Length != _skillDataBase.Skillbool.Length ||
            loadSkillData.SkillSetNo.Length != _skillDataBase.SkillSetNo.Length)
        {
            for (int i = 0; i < _skillDataBase.Skillbool.Length; i++)
            {
                _skillDataBase.Skillbool[i] = false;
            }

            _skillDataBase.SkillSetNo = new int[_skillDataBase.SkillSetNo.Length];

            _skillDataBase.SkillPoint = 0;
        }
        //読み込めたらデータを渡す
        else
        {
            for (int i = 0; i < _skillDataBase.SkillData.Length; i++)
            {
                _skillDataBase.Skillbool[i] = loadSkillData.SkillBool[i];
            }
            for (int j = 0; j < _skillDataBase.SkillSetNo.Length; j++)
            {
                _skillDataBase.SkillSetNo[j] = loadSkillData.SkillSetNo[j];
            }

            _skillDataBase.SkillPoint = loadSkillData.SkillPoint;
        }
    }

    private void OnDisable()
    {
        DataSave();
    }

    /// <summary>
    /// データの保存を行うクラス
    /// </summary>
    private void DataSave()
    {
        var skillSaveData = new SkillData
        {
            SkillBool = _skillDataBase.Skillbool,
            SkillSetNo = _skillDataBase.SkillSetNo,
            SkillPoint = _skillDataBase.SkillPoint
        };

        DataSaveManager.Instance.Save(skillSaveData, FILE_NAME);
    }
}

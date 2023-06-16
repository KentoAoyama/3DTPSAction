using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetSample : MonoBehaviour
{
    [Header("Skill���Z�b�g����{�^���̃I�u�W�F�N�g"),Tooltip("Skill���Z�b�g����{�^���̃I�u�W�F�N�g"),SerializeField]
    GameObject[] _skillPanel;

    [Header("�{�^����Prehab"),Tooltip("�{�^����Prehab"),SerializeField]
    GameObject _skillbottom;

    [Header("�擾���Ă���X�L������ׂ�ꏊ"),Tooltip("�擾���Ă���X�L������ׂ�ꏊ"),SerializeField]
    GameObject _skillSetPoint;

    private SkillDataBase _dataBase;

    [Header("�X�L���̐����������ꏊ"), Tooltip("�X�L���̐����������ꏊ"), SerializeField]
    Text _tutorialText;

    [Header("���݉���I�����Ă��邩�������ꏊ"), Tooltip("���݉���I�����Ă��邩�������ꏊ"), SerializeField]
    Text _skillText;

    [Header("�I�𒆂̃X�L����No(�X�L���f�[�^�̗v�f��)"), Tooltip("�I�𒆂̃X�L����No(�X�L���f�[�^�̗v�f��)"), SerializeField]
    public int _tmp = -1;

    private void Start()
    {
        _dataBase = SkillDataBase.Instance;
        for (var i = 0; i < _skillPanel.Length; i++)
        {
            var num = i;
            var button = _skillPanel[i].GetComponent<Button>();
            button.onClick.AddListener(() => MoveSkillChoice(num));
        }   //Skill���Z�b�g����{�^����onClick��ݒ肷��B
        if (_skillPanel.Length != 0)
        {
            for (var i = 0; i < _skillPanel.Length; i++)
            {
                var text = _skillPanel[i].GetComponentInChildren<Text>();
                text.text = _dataBase.SkillData[_dataBase.SkillSetNo[i]].SkillName;
            }   //���݃Z�b�g���Ă���X�L���̖��O�������B
        }

        SkillSet(_dataBase.Skillbool, _dataBase.SkillData);
    }

    private void OnEnable()
    {
        foreach (Transform trans in _skillSetPoint.gameObject.transform)
        {
            Destroy(trans.gameObject);
        }   //���ׂ��X�L�������ׂč폜

        //�X�L������ׂ鏈��
        SkillSet(_dataBase.Skillbool, _dataBase.SkillData);
    }

    /// <summary>�擾�����X�L������ׂ郁�\�b�h</summary>
    /// <param name="skillbool">�擾���Ă���X�L�����m�F����bool�̔z��</param>
    /// <param name="skillObjs">�X�L���ꗗ�̏��</param>
    public void SkillSet(bool[] skillbool, SkillObject[] skillObjs)
    {
        for (var i = 0; i < skillbool.Length; i++)
        {
            if (skillbool[i])
            {
                //�{�^���𐶐����ă{�^���̕ϐ���錾
                var button = Instantiate(_skillbottom, transform.position, Quaternion.identity);
                if (button)
                {
                    //�{�^����_skillSetPoint(Scrollview/Viewport/Content)�̒����ɔz�u����B
                    button.transform.SetParent(_skillSetPoint.transform);
                    //�{�^���̃e�L�X�g�Ƀf�[�^�x�[�X�ɓo�^����Ă���X�L���̖��O����́B
                    var text = button.GetComponentInChildren<Text>();
                    if (text) text.text = skillObjs[i].SkillName;
                    //�{�^���ɃX�L���̗v�f���������Ă����B
                    var click = button.GetComponent<Button>();
                    var num = i;
                    click.onClick.AddListener(() => SelectSkill(num));
                }
            }
        }
    }

    /// <summary>�X�L����I�������Ƃ��ɌĂ΂�郁�\�b�h</summary>
    /// <param name="i">�I�������X�L����No</param>
    public void SelectSkill(int i)
    {
        //�X�L���̏��̕\��
        SkillDis(i, _dataBase.SkillData);
    }

    /// <summary>�I�������X�L���̏��̃��Z�b�g</summary>
    public void SelectSkillReset()
    {
        _tutorialText.text = "";
        _skillText.text = "";
        _tmp = -1;
    }

    /// <summary>�X�L���̏���\�����郁�\�b�h</summary>
    /// <param name="i">�I�������X�L����No</param>
    /// <param name="skillobj">�X�L���ꗗ�̏��</param>
    void SkillDis(int i, SkillObject[] skillobj)
    {
        _tmp = i;
        _tutorialText.text = skillobj[i].Description;
        _skillText.text = $"{skillobj[i].SkillName} ��I��";
    }

    /// <summary>�Z�b�g����X�L���̃{�^�������������̏���</summary>
    /// <param name="i">�I�������X�L�����Z�b�g����ꏊ��No</param>
    public void MoveSkillChoice(int i)
    {
        if (_tmp == -1) { return; }
        MoveSkill(i, _dataBase.SkillSetNo, _dataBase.SkillData[_tmp]);
    }

    /// <summary>�X�L�����Z�b�g���郁�\�b�h</summary>
    /// <param name="i">�I�������X�L�����Z�b�g����ꏊ��No</param>
    /// <param name="SetNo">�X�L�����Z�b�g����ꏊ�̏��</param>
    /// <param name="_skill">�Z�b�g����X�L���̏��</param>
    public void MoveSkill(int i, int[] SetNo, SkillObject _skill)
    {
        var text = _skillPanel[i].GetComponentInChildren<Text>();
        text.text = _skill.SkillName;
        SetNo[i] = _tmp;
    }
}

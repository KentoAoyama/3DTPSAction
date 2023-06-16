using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeSetChangeScripts : MonoBehaviour
{
    
    [SerializeField] 
    GameObject _treePanel;
    [SerializeField] 
    GameObject _skillSetPanel;
    [SerializeField] 
    Text _selectText;
    //SkillSetScripts _skillSetScript;
    [SerializeField]
    bool _setbool;
    [SerializeField]
    string _treeText = "�X�L���c���[";
    [SerializeField]
    string _setText = "�X�L���Z�b�g";

    // Start is called before the first frame update
    void Start()
    {
        _skillSetPanel.SetActive(true);
        _treePanel.SetActive(false);
    }

    /// <summary>�X�L���Z�b�g���j���[����X�L���c���[���j���[�ɕς���Ƃ��̏����B</summary>
    public void TreeChange()
    {
        _skillSetPanel.SetActive(false);
        _treePanel.SetActive(true);
        _selectText.text = _treeText;
    }

    /// <summary>�X�L���c���[���j���[����X�L���Z�b�g���j���[�ɕς���Ƃ��̏����B</summary>
    public void SkillSetChange()
    {
        _skillSetPanel.SetActive(true);
        _treePanel.SetActive(false);
        //_skillSetScript = GameObject.FindAnyObjectByType<SkillSetScripts>();
        if (_setbool == true)
        {
            //_selectText.text = _skillSetScript.SkillType.ToString();
        }
        else
        {
            _selectText.text = _setText;
        }
        
    }
}

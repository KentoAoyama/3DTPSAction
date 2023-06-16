using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField]
    private Text _skillPointText;

    [SerializeField]
    private Text _lifePointText;
    public Text LifePointText => _lifePointText;

    [SerializeField]
    private GameObject _guiPanel;
    public GameObject GUIPanel => _guiPanel;

    [SerializeField]
    private GameObject _titlePanel;
    public GameObject TitlePanel => _titlePanel;

    [SerializeField]
    private GameObject _gameOverPanel;
    public GameObject GameOverPanel => _gameOverPanel;

    private void Start()
    {
        _guiPanel.SetActive(false);
        _titlePanel.SetActive(true);
        _gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        _skillPointText.text = SkillDataBase.Instance.SkillPoint.ToString("00");
        
    }

    public void GameStart()
    {
        _guiPanel.SetActive(true);
        _titlePanel.SetActive(false);
    }
}

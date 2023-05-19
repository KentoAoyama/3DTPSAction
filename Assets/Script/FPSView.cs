using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSView : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        _text.text = (1f / Time.deltaTime).ToString("00");
    }
}

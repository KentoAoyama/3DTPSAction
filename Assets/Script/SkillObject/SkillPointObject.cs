using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _sphere = null;

    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;

        if (other.gameObject.CompareTag("Player"))
        {
            _sphere.SetActive(false);
            SkillDataBase.Instance.SkillPoint += 3;

            _isActive = false;
        }
    }
}

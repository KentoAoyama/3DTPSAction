using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerAttack
{
    [Tooltip("�ˏo����e��prefab")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("�e���ˏo������W")]
    [SerializeField]
    private float _muzzleLength = 5f;

    [Tooltip("�e�̑��x")]
    [SerializeField]
    private float _bulletSpeed = 300f;

    [Tooltip("�e�̎ˌ��̃C���^�[�o��")]
    [SerializeField]
    private float _shootInterval = 1f;

    [Tooltip("�ˌ�����ۂɒ��e�n�_�����߂�ۂɌ���Ray�̒���")]
    [SerializeField]
    private float _rayLength = 100f;

    [Tooltip("�N���X�w�A��Image")]
    [SerializeField]
    private Image _crassHair;

    [Tooltip("����")]
    [SerializeField]
    private GameObject _sphere;

    [Tooltip("�ˌ����s���n�_�Ɉړ�������I�u�W�F�N�g")]
    [SerializeField]
    private Transform _shootPos;
    public Transform ShootPos => _shootPos;

    private float _intervalTimer = 0f;


    /// <summary>
    /// �v���C���[�̎ˌ�����
    /// </summary>
    public void BulletShoot(bool isAim, bool isShoot, Vector3 playerPos)
    {
        //�C���^�[�o���ɃJ�E���g�����Z
        _intervalTimer += Time.deltaTime;

        if (!isAim)
        {
            if (_sphere != null)
                _sphere.SetActive(false);
            return;
        }
        else
        {
            if (_sphere != null)
                _sphere.SetActive(true);
        }

        if (isShoot && _shootInterval < _intervalTimer)
        {
            //�e�𐶐�
            BulletController bulletController = Object.Instantiate(_bullet).GetComponent<BulletController>();
            GameObject bullet = bulletController.gameObject;

            playerPos.y += 1;

            // Ray�������A�������Ă����炻�̍��W�Ɍ�����
            Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
            if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
            {
                var muzzlePos = hit.point - playerPos;
                muzzlePos = muzzlePos.normalized * _muzzleLength + playerPos;
                bullet.transform.position = muzzlePos;
                bullet.transform.forward = hit.point - muzzlePos;
            }
            //�������Ă��Ȃ���΁ARay�̏I���_�Ɍ������Č���
            else
            {
                var muzzlePos = hit.point - playerPos;
                muzzlePos = muzzlePos.normalized * _muzzleLength + playerPos;
                bullet.transform.position = muzzlePos;
                bullet.transform.forward = Camera.main.transform.forward * _rayLength - muzzlePos;
            }

            //�e�𓮂���
            bullet.GetComponent<BulletController>().Shoot(_bulletSpeed);

            //�C���^�[�o�������Z�b�g
            _intervalTimer = 0f;
        }
    }

    public void ShootPositionSet()
    {
        // Ray�������A�������Ă����炻�̍��W�Ɍ�����
        Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
        {
            _shootPos.position = hit.point;
        }
        //�������Ă��Ȃ���΁ARay�̏I���_�Ɍ������Č���
        else
        {
            _shootPos.position = Camera.main.transform.forward * _rayLength;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerAttack
{
    [Tooltip("射出する弾のprefab")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("弾を射出する座標")]
    [SerializeField]
    private float _muzzleLength = 5f;

    [Tooltip("弾の速度")]
    [SerializeField]
    private float _bulletSpeed = 300f;

    [Tooltip("弾の射撃のインターバル")]
    [SerializeField]
    private float _shootInterval = 1f;

    [Tooltip("射撃する際に着弾地点を決める際に撃つRayの長さ")]
    [SerializeField]
    private float _rayLength = 100f;

    [Tooltip("クロスヘアのImage")]
    [SerializeField]
    private Image _crassHair;

    [Tooltip("球体")]
    [SerializeField]
    private GameObject _sphere;

    [Tooltip("射撃を行う地点に移動させるオブジェクト")]
    [SerializeField]
    private Transform _shootPos;
    public Transform ShootPos => _shootPos;

    private float _intervalTimer = 0f;


    /// <summary>
    /// プレイヤーの射撃処理
    /// </summary>
    public void BulletShoot(bool isAim, bool isShoot, Vector3 playerPos)
    {
        //インターバルにカウントを加算
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
            //弾を生成
            BulletController bulletController = Object.Instantiate(_bullet).GetComponent<BulletController>();
            GameObject bullet = bulletController.gameObject;

            playerPos.y += 1;

            // Rayを撃ち、当たっていたらその座標に向ける
            Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
            if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
            {
                var muzzlePos = hit.point - playerPos;
                muzzlePos = muzzlePos.normalized * _muzzleLength + playerPos;
                bullet.transform.position = muzzlePos;
                bullet.transform.forward = hit.point - muzzlePos;
            }
            //当たっていなければ、Rayの終着点に向かって撃つ
            else
            {
                var muzzlePos = hit.point - playerPos;
                muzzlePos = muzzlePos.normalized * _muzzleLength + playerPos;
                bullet.transform.position = muzzlePos;
                bullet.transform.forward = Camera.main.transform.forward * _rayLength - muzzlePos;
            }

            //弾を動かす
            bullet.GetComponent<BulletController>().Shoot(_bulletSpeed);

            //インターバルをリセット
            _intervalTimer = 0f;
        }
    }

    public void ShootPositionSet()
    {
        // Rayを撃ち、当たっていたらその座標に向ける
        Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
        {
            _shootPos.position = hit.point;
        }
        //当たっていなければ、Rayの終着点に向かって撃つ
        else
        {
            _shootPos.position = Camera.main.transform.forward * _rayLength;
        }
    }
}

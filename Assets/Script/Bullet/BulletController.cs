using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Tooltip("�e���j�������܂ł̎���")]
    [SerializeField]
    private float _destroyInterval = 5f;

    public void Shoot(float bulletSpeed)
    {
        GetComponent<Rigidbody>()
            .AddForce(
                bulletSpeed * Time.deltaTime * transform.forward,
                ForceMode.Impulse);

        StartCoroutine(DestroyInterval());
    }

    private IEnumerator DestroyInterval()
    {
        yield return new WaitForSeconds(_destroyInterval);

        Destroy(gameObject);
    }
}

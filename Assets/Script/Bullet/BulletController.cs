using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Tooltip("’e‚ª”jŠü‚³‚ê‚é‚Ü‚Å‚ÌŽžŠÔ")]
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

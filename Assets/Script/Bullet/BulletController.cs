using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Tooltip("�e���j�������܂ł̎���")]
    [SerializeField]
    private float _destroyInterval = 5f;

    private int _damage = 1;

    public void Shoot(float bulletSpeed, int skillDamage)
    {
        GetComponent<Rigidbody>()
            .AddForce(
                bulletSpeed * transform.forward,
                ForceMode.Impulse);

        _damage += skillDamage;
        StartCoroutine(DestroyInterval());
    }

    private IEnumerator DestroyInterval()
    {
        yield return new WaitForSeconds(_destroyInterval);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable hit) && !other.gameObject.CompareTag("Player"))
        {
            hit.Hit(_damage);
        }

        Destroy(gameObject);
    }
}

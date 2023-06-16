using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHittable
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _life = 5;

    [SerializeField]
    private float _attackInterval = 2f;

    [SerializeField]
    private float _moveLength = 50f;

    private InGameController _inGameController;
    private PlayerController _player;

    private bool _isStop = false;

    public void Hit(int damage)
    {
        _life--;

        if (_life <= 0)
            Destroy(gameObject);
    }

    private void Start()
    {
        _inGameController = FindObjectOfType<InGameController>().GetComponent<InGameController>();
        _player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_isStop || _inGameController.State != InGameController.GameState.InGame) return;

        if ((_player.gameObject.transform.position - transform.position).sqrMagnitude > _moveLength) return;

        var dir = (_player.gameObject.transform.position - transform.position).normalized;
        transform.forward = dir;
        transform.Translate(_speed * Time.deltaTime * dir, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable hit))
        {
            hit.Hit(1);

            Debug.Log("OK");

            StartCoroutine(AttackDelay());
        }
    }

    private IEnumerator AttackDelay()
    {
        _isStop = true;

        yield return new WaitForSeconds(_attackInterval);

        _isStop = false;
    }
}

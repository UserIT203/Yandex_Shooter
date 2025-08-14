using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToDiactivate;
    [SerializeField] private string _targetTag;

    private float _damage;
    private Rigidbody _rigidbody;
    private CustomPool<Bullet> _pool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _targetTag)
        {
            Enemy enemy;
            if(other.TryGetComponent<Enemy>(out enemy))
            {
                enemy.Stats.TakeDamage(_damage);
            }

            Player player;
            if(other.TryGetComponent<Player>(out player))
                player.Stats.TakeDamage(_damage);

            _pool.Release(this);
        }
    }

    public void Shoot(Vector3 direction, CustomPool<Bullet> pool, float damage)
    {
        if (_pool == null)
            _pool = pool;

        _damage = damage;
        _rigidbody.velocity = direction * _speed;

        StartCoroutine(Diactivate());
    }

    private IEnumerator Diactivate()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_timeToDiactivate);

        yield return waitingTime;

        _pool.Release(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _timeToLife;
    [SerializeField] private GameObject _rotationObject;
    [SerializeField] private LayerMask _enemiesLayer;

    private float _damage, _attackRadius, _attackDelay, _attackTime;
    private CustomPool<Bullet> _bulletPool;
    private IDamagable _currentTarget;

    private void Start()
    {
        StartCoroutine(DestroyTurret());
    }

    
    private void Update()
    {
        Rotate();
        Attack();
    }

    public void Initialized(float damage, float radius, float _attaclDelay, CustomPool<Bullet> bulletPool)
    {
        _damage = damage;
        _attackRadius = radius;
        _bulletPool = bulletPool;
        _attackDelay = _attaclDelay;
    }

    private Vector3 GetTargetDirection()
    {
        Collider[] enemies = Physics.OverlapSphere(
            transform.position, _attackRadius, _enemiesLayer);
        
        float distanceTemp = float.MaxValue;
        Vector3 direction = Vector3.zero;

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
        
            if(distance < distanceTemp)
            {
                distanceTemp = distance;
                direction = (enemy.transform.position - transform.position).normalized;
            }
        }

        return direction;
    }

    private void Attack()
    {
        _attackTime -= Time.deltaTime;

        if (_attackTime > 0) return;

        Vector3 direction = GetTargetDirection();

        if (direction == Vector3.zero) return;

        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.Shoot(direction, _bulletPool, _damage);

        _attackTime = _attackDelay;
    }

    private IEnumerator DestroyTurret()
    {
        yield return new WaitForSeconds(_timeToLife);

        Destroy(gameObject);
    }

    private void Rotate()
    {
        Vector3 direction = GetTargetDirection();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rotationObject.transform.rotation = Quaternion.Euler(90, 0, angle);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeCombat : EnemyCombat
{
    [Header("Ranged Settigns")]
    [SerializeField] private Transform _firePoint;

    private CustomPool<Bullet> _bulletPool;

    protected override void Initialized(Player player, CustomPool<Bullet> bulletPool = null)
    {
        base.Initialized(player, bulletPool);
        _bulletPool = bulletPool;
    }

    protected override void Attack()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        direction.y = 0;
        Shoot(direction);
    }

    private void Shoot(Vector3 direction)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = _firePoint.position;
        bullet.Shoot(direction, _bulletPool, _enemy.Stats.Damage.GetValue());
    }
}

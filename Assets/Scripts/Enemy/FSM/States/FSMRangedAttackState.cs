using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMRangedAttackState : FSMMelleAttack
{
    private Transform _firePoint;
    private CustomPool<Bullet> _bulletPool;

    public FSMRangedAttackState(FSM fsm, EnemyUnit boos, Player player, 
        NavMeshAgent agent, CustomPool<Bullet> bulletPool, Transform firePoint) : base(fsm, boos, player, agent)
    {
        _bulletPool = bulletPool;
        _firePoint = firePoint;
    }

    protected override void Attack()
    {
        if (_attackTimer > 0) return;

        Vector3 direction = (_player.transform.position - _unit.transform.position).normalized;
        direction.y = 0;

        Shoot(direction);

        _attackTimer = _unit.Stats.AttackDealy.GetValue();
    }

    private void Shoot(Vector3 direction)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = _firePoint.position;
        bullet.Shoot(direction, _bulletPool, _unit.Stats.Damage.GetValue());
    }
}

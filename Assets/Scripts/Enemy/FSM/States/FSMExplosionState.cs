using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FSMExplosionState : FSMMelleAttack
{
    private float _radiusExplosion;

    public FSMExplosionState(FSM fsm, EnemyUnit boos, Player player, 
        NavMeshAgent agent, float radiusExplosion) : base(fsm, boos, player, agent)
    {
        _radiusExplosion = radiusExplosion;
    }

    protected override void Attack()
    {
        if (_attackTimer > 0) return;

        Explosion();
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(_unit.transform.position, _radiusExplosion);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<IDamagable>(out var target))
            {
                target.TakeDamage(_unit.Stats.Damage.GetValue());
            }
        }

        _unit.Stats.TakeDamage(1000);
    }
}

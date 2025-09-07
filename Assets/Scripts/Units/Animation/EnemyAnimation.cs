using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : UnitAnimation
{
    protected EnemyUnit _enemyUnit;
    private NavMeshAgent _agent;

    private void OnDisable()
    {
        _enemyUnit.onAttack -= AttackAnimation;
        _enemyUnit.Stats.onDie -= DieAnimation;
    }

    protected override void Initialized()
    {
        base.Initialized();
        _agent = transform.root.GetComponent<NavMeshAgent>();
        _enemyUnit = transform.root.GetComponent<EnemyUnit>();

        _enemyUnit.Stats.onDie += DieAnimation;
        _enemyUnit.onAttack += AttackAnimation;
    }

    protected override void SetUnitSpeed()
    {
        MoveAnimation(_agent.velocity.magnitude);
    }

    protected override void AttackAnimation()
    {
        _animator.SetTrigger("onAttack");
    }

    protected override void DieAnimation()
    {
        _animator.SetBool("isDead", true);
    }

    public override void DieAction()
    {
        Destroy(transform.root.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class FSMMelleAttack : FSMState
{
    private NavMeshAgent _agent;

    protected EnemyUnit _unit;
    protected Player _player;
    protected float _attackTimer;

    public FSMMelleAttack(FSM fsm, EnemyUnit boos, Player player, NavMeshAgent agent) : base(fsm)
    {
        _unit = boos;
        _player = player;
        _agent = agent;
    }

    public override void Enter()
    {
        Debug.Log("Melle Attack [ENTER]");
        _agent.isStopped = true;
    }
    
    public override void Exit() 
    {
        Debug.Log("Melle Attack [EXIT]");
        _agent.isStopped = false;
    }

    public override void Update()
    {
        _attackTimer -= Time.deltaTime;
        Attack();
    }

    protected virtual void Attack()
    {
        if(_attackTimer > 0) return;

        _player.TakeDamage(_unit.Stats.Damage.GetValue());
        _attackTimer = _unit.Stats.AttackDealy.GetValue();
        Debug.Log("Boss Attack Player");
    }
}

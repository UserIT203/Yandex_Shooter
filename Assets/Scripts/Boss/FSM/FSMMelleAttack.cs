using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class FSMMelleAttack : FSMState
{
    private Boss _boss;
    private Player _player;
    private NavMeshAgent _agent;
    private float _attackTimer;

    public FSMMelleAttack(FSM fsm, Boss boos, Player player, NavMeshAgent agent) : base(fsm)
    {
        _boss = boos;
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

        _player.TakeDamage(_boss.Stats.Damage.GetValue());
        _attackTimer = _boss.Stats.AttackDealy.GetValue();
        Debug.Log("Boss Attack Player");
    }
}

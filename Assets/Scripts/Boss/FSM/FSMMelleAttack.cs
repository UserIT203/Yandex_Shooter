using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMMelleAttack : FSMState
{
    private Boss _boss;
    private Player _player;
    private NavMeshAgent _agent;

    public FSMMelleAttack(FSM fsm, Boss boos, Player player, NavMeshAgent agent) : base(fsm)
    {
        _boss = boos;
        _player = player;
        _agent = agent;
    }

    public override void Enter()
    {
        _agent.isStopped = true;
    }
    
    public override void Exit() 
    {
        _agent.isStopped = false;
    }

    public override void Update()
    {
        
    }

    protected virtual void HasAttack()
    {

    }
}

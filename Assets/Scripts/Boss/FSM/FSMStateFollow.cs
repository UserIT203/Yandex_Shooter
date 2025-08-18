using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMStateFollow : FSMState
{
    private readonly NavMeshAgent _agent;
    private readonly Player _target;
    private readonly Boss _boss;

    public FSMStateFollow(FSM fsm, NavMeshAgent agent, Player player, 
        Boss boss) : base(fsm)
    {
        _agent = agent;
        _target = player;
        _boss = boss;

        _agent.stoppingDistance = _boss.Stats.RadiusAttack.GetValue();
    }

    public override void Enter()
    {
        Debug.Log("Follow State [ENTER]");
        _agent.isStopped = false;
    }

    public override void Exit()
    {
        Debug.Log("Follow State [EXIT]");
        _agent.isStopped = true;
    }

    public override void Update()
    {
        _agent.stoppingDistance = _boss.Stats.RadiusAttack.GetValue();
        _agent.speed = _boss.Stats.Speed.GetValue();

        _agent.SetDestination(_target.transform.position);
    }
}

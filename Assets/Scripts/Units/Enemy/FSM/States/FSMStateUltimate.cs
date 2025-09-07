using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMStateUltimate : FSMState
{
    private IUltimate _ultimate;
    private NavMeshAgent _agent;
    private Boss _boss;

    public FSMStateUltimate(FSM fsm, NavMeshAgent agent , IUltimate ultimate, Boss boss) : base(fsm)
    {
        _agent = agent;
        _ultimate = ultimate;
        _boss = boss;
    }

    public override void Enter()
    {
        _agent.isStopped = true;
        _ultimate.TryUse();
        _ultimate.onUltimateEnd += UtimateEnd;
        _boss.OnUltimate(true);
    }

    public override void Exit() 
    {
        _agent.isStopped = false;
        _ultimate.onUltimateEnd -= UtimateEnd;
        _boss.OnUltimate(false);
    }

    private void UtimateEnd()
    {
        Debug.Log("End State");
        _fsm.SetState<FSMStateFollow>();
    }
}

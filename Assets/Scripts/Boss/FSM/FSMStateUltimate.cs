using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMStateUltimate : FSMState
{
    private IUltimate _ultimate;
    private NavMeshAgent _agent;

    public FSMStateUltimate(FSM fsm, NavMeshAgent agent , IUltimate ultimate) : base(fsm)
    {
        _agent = agent;
        _ultimate = ultimate;
    }

    public override void Enter()
    {
        _agent.isStopped = true;
        _ultimate.TryUse();
        _ultimate.onUltimateEnd += UtimateEnd;
    }

    public override void Exit() 
    {
        _agent.isStopped = false;
        _ultimate.onUltimateEnd -= UtimateEnd;
    }

    private void UtimateEnd()
    {
        Debug.Log("End State");
        _fsm.SetState<FSMStateFollow>();
    }
}

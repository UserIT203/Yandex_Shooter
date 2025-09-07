using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : Enemy
{
    [SerializeField] private float _radiusExplosion;

    protected override void InitializedFSM()
    {
        _fsm = new FSM();

        _fsm.AddFsm(new FSMExplosionState(_fsm, this, _player, _agent, _radiusExplosion));
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));

        #region FollowState Transition

        _fsm.AddTransition<FSMStateFollow, FSMExplosionState>(
            new FuncPredicate(HasAttack));
        #endregion

        _fsm.SetState<FSMStateFollow>();
    }
}

using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Enemy : EnemyUnit
{
    protected override void InitializedFSM()
    {
        _fsm = new FSM();

        _fsm.AddFsm(new FSMMelleAttack(_fsm, this, _player, _agent));
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));

        #region FollowState Transition

        _fsm.AddTransition<FSMStateFollow, FSMMelleAttack>(
            new FuncPredicate(HasAttack));
        #endregion

        #region AttackState Transition

        _fsm.AddTransition<FSMMelleAttack, FSMStateFollow>(
            new FuncPredicate(HasFollow));
        #endregion

        _fsm.SetState<FSMStateFollow>();
    }

    protected override void Die()
    {
        _observer.OnEnemyDestroed();
        base.Die();
    }
}

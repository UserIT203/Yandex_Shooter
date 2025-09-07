using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] private Transform _firePoint;

    protected override void InitializedFSM()
    {
        _fsm = new FSM();

        Debug.Log(_bulletPool + "Enemy " + gameObject.name);
        _fsm.AddFsm(new FSMRangedAttackState(_fsm, this, _player, _agent, _bulletPool, _firePoint));
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));

        #region FollowState Transition

        _fsm.AddTransition<FSMStateFollow, FSMRangedAttackState>(
            new FuncPredicate(HasAttack));
        #endregion

        #region AttackState Transition

        _fsm.AddTransition<FSMRangedAttackState, FSMStateFollow>(
            new FuncPredicate(HasFollow));
        #endregion

        _fsm.SetState<FSMStateFollow>();
    }
}

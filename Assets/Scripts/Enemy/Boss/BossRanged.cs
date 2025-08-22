using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRanged : Boss
{
    [SerializeField] private Transform _firePoint;

    protected override void InitializedFSM()
    {
        _fsm = new FSM();
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));
        _fsm.AddFsm(new FSMRangedAttackState(_fsm, this, _player, _agent, _bulletPool, _firePoint));
        _fsm.AddFsm(new FSMStateUltimate(_fsm, _agent, _bossConfig.Ultimate));

        #region FollowState
        _fsm.AddTransition<FSMStateFollow, FSMRangedAttackState>(
            new FuncPredicate(HasAttack));
        _fsm.AddTransition<FSMStateFollow, FSMStateUltimate>(
            new FuncPredicate(_bossConfig.Ultimate.CanUse));
        #endregion

        #region AttackState
        _fsm.AddTransition<FSMRangedAttackState, FSMStateFollow>(
            new FuncPredicate(HasFollow));
        _fsm.AddTransition<FSMRangedAttackState, FSMStateUltimate>(
            new FuncPredicate(_bossConfig.Ultimate.CanUse));
        #endregion

        _fsm.SetState<FSMStateFollow>();
    }
}

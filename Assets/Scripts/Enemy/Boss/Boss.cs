using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;


public class Boss : EnemyUnit
{
    protected BossConfig _bossConfig;

    protected override void Update()
    {
        base.Update();
        _bossConfig.Ultimate.Update();
    }

    public override void Initialized(Player player, IEnemyObserver observer, CustomPool<Bullet> bulletPool)
    {
        _bossConfig = _config as BossConfig;

        _bossConfig.Ultimate.Initialized(player);
        _bossConfig.Ultimate.SetBoss(this);

        base.Initialized(player, observer, bulletPool);
    }

    protected override void InitializedFSM()
    {
        _fsm = new FSM();
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));
        _fsm.AddFsm(new FSMMelleAttack(_fsm, this, _player, _agent));
        _fsm.AddFsm(new FSMStateUltimate(_fsm, _agent, _bossConfig.Ultimate));

        #region FollowState
        _fsm.AddTransition<FSMStateFollow, FSMMelleAttack>(
            new FuncPredicate(HasAttack));
        _fsm.AddTransition<FSMStateFollow, FSMStateUltimate>(
            new FuncPredicate(_bossConfig.Ultimate.CanUse));
        #endregion

        #region AttackState
        _fsm.AddTransition<FSMMelleAttack, FSMStateFollow>(
            new FuncPredicate(HasFollow));
        _fsm.AddTransition<FSMMelleAttack, FSMStateUltimate>(
            new FuncPredicate(_bossConfig.Ultimate.CanUse));
        #endregion

        _fsm.AddTransition<FSMStateUltimate, FSMMelleAttack>(
            new FuncPredicate(HasAttack));

        _fsm.SetState<FSMStateFollow>();
    }

    protected override void Die()
    {
        _observer.OnBossDestroed();
        base.Die();
    }
}

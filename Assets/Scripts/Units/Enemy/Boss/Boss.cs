using UnityEngine;
using System;
using System.Collections.Generic;

public class Boss : EnemyUnit
{
    [field: SerializeField] public Sprite BossIcon { get; private set; }
    [field: SerializeField] public TranslatingText BossName { get; private set; }

    [SerializeField] protected List<EnemyInSpawner> _spawnEnemies;

    protected BossConfig _bossConfig;
    protected Spawner _spawner;
    protected bool _canSpawnEnemies = false;

    public event Action<bool> onUltimate;

    protected override void Update()
    {
        base.Update();
        _bossConfig.Ultimate.Update();
    }

    public override void Initialized(Player player, IEnemyObserver observer, CustomPool<Bullet> bulletPool, ItemUseContext context, GameTimeManager timeManager, Spawner spawner)
    {
        _bossConfig = _config as BossConfig;
        _bossConfig.Ultimate.Initialized(player);
        _bossConfig.Ultimate.SetBoss(this);

        base.Initialized(player, observer, bulletPool, context, timeManager);

        _spawner = spawner;
        Stats.onHalfHealth += SpawnEnemies;
    }

    protected override void InitializedFSM()
    {
        _fsm = new FSM();
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));
        _fsm.AddFsm(new FSMMelleAttack(_fsm, this, _player, _agent));
        _fsm.AddFsm(new FSMStateUltimate(_fsm, _agent, _bossConfig.Ultimate, this));

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
        AudioManager.PlaySound("BossDie");
        _observer.OnBossDestroed();
        _bossConfig.Ultimate.StopActions();
        base.Die();
    }

    private void SpawnEnemies()
    {
        Debug.LogWarning("Boss as half HP");
        if(_canSpawnEnemies == false)
        {
            _spawner.CreateEnemiesFromBoss(_spawnEnemies);
            _canSpawnEnemies = true;
        }
    }

    public void OnUltimate(bool state) => onUltimate?.Invoke(state);
}

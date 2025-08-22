using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Boss : MonoBehaviour, IDamagable
{
    [SerializeField] private BossConfig _config;

    private FSM _fsm;
    private EnemyStats _enemyStats;
    private NavMeshAgent _agent;
    private Player _player;
    private IEnemyObserver _observer;
    private CustomPool<Bullet> _bulletPool;

    public EnemyStats Stats => _enemyStats;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;

        _config.Ultimate.Update();
        _fsm.Update();
    }

    public void TakeDamage(float damage)
    {
        _enemyStats.TakeDamage(damage);
    }

    public void Initialized(Player player, IEnemyObserver observer, 
        CustomPool<Bullet> bulletPool)
    {
        _enemyStats = new EnemyStats(_config);
        _observer = observer;
        _player = player;
        _config.Ultimate.Initialized(player);
        _config.Ultimate.SetBoss(this);

        InitializedFSM();

        _enemyStats.onDie += Die;
    }

    private void InitializedFSM()
    {
        _fsm = new FSM();
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));
        _fsm.AddFsm(new FSMMelleAttack(_fsm, this, _player, _agent));
        _fsm.AddFsm(new FSMStateUltimate(_fsm, _agent, _config.Ultimate));

        #region FollowState
        _fsm.AddTransition<FSMStateFollow, FSMMelleAttack>(
            new FuncPredicate(HasAttack));
        _fsm.AddTransition<FSMStateFollow, FSMStateUltimate>(
            new FuncPredicate(_config.Ultimate.CanUse));
        #endregion

        #region AttackState
        _fsm.AddTransition<FSMMelleAttack, FSMStateFollow>(
            new FuncPredicate(HasFollow));
        _fsm.AddTransition<FSMMelleAttack, FSMStateUltimate>(
            new FuncPredicate(_config.Ultimate.CanUse));
        #endregion

        _fsm.AddTransition<FSMStateUltimate, FSMMelleAttack>(
            new FuncPredicate(HasAttack));

        _fsm.SetState<FSMStateFollow>();
    }

    private bool HasAttack()
    {
        float distance = Vector3.Distance(_player.transform.position,
            transform.position);

        return distance <= _config.RadiusAttack.GetValue();
    }

    private bool HasFollow()
    {
        float distance = Vector3.Distance(_player.transform.position,
            transform.position);

        return distance > _config.RadiusAttack.GetValue();
    }

    private void Die()
    {
        _observer.OnBossDestroed();
        Destroy(gameObject);
    }
}

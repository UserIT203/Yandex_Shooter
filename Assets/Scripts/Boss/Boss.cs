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
        _fsm.Update();
    }

    public void Initialized(Player player, IEnemyObserver observer, 
        CustomPool<Bullet> bulletPool)
    {
        _enemyStats = new EnemyStats(_config);
        _observer = observer;
        _player = player;

        _enemyStats.onDie += Die;

        InitializedFSM();
    }

    private void InitializedFSM()
    {
        _fsm = new FSM();
        _fsm.AddFsm(new FSMStateFollow(_fsm, _agent, _player, this));
        _fsm.SetState<FSMStateFollow>();
    }

    private void Die()
    {
        _observer.OnBossDestroed();
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        _enemyStats.TakeDamage(damage);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LootBag))]
public abstract class EnemyUnit : MonoBehaviour, IDamagable
{
    [SerializeField] protected EnemyConfig _config;

    protected FSM _fsm;
    protected Player _player;
    protected IEnemyObserver _observer;
    protected CustomPool<Bullet> _bulletPool;
    protected LootBag _lootBag;
    protected ItemUseContext _context;
    protected NavMeshAgent _agent;

    private bool _isDead = false;
    private bool _isFreeze = false;
    private GameTimeManager _timeManager;

    public event Action<float> onTakeDamage;
    public event Action onAttack;

    public EnemyStats Stats { get; private set; }

    private void OnDisable()
    {
        _timeManager.OnGamePaused -= Freeze;
        _timeManager.OnGameResumed -= Unfreeze;
    }

    private void Awake()
    {
        Stats = new EnemyStats(_config);
        Stats.onDie += Die;

        _lootBag = GetComponent<LootBag>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void OnAttack() => onAttack?.Invoke();

    private void Freeze()
    {
        _isFreeze = true;
        _agent.isStopped = true;
    }
    private void Unfreeze()
    {
        Debug.Log("Enemy Unfreeze");

        _isFreeze = false;
        _agent.isStopped = false;
    }

    protected virtual void Update()
    {
        if (_isFreeze == true || _isDead == true) return;

        _fsm.Update();
    }

    protected virtual void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public virtual void TakeDamage(float damage)
    {
        onTakeDamage?.Invoke(damage);
        Stats.TakeDamage(damage);
    }

    public virtual void Initialized(Player player, IEnemyObserver observer,
        CustomPool<Bullet> bulletPool, ItemUseContext context, GameTimeManager timeManager
        , Spawner spawner = null)
    {
        _player = player;
        _observer = observer;
        _bulletPool = bulletPool;
        _context = context;

        _timeManager = timeManager;
        _timeManager.OnGamePaused += Freeze;
        _timeManager.OnGameResumed += Unfreeze;

        InitializedFSM();
    }

    protected virtual void Die()
    {
        _lootBag.CreateItems(_player, _context);

        _agent.isStopped = true;
        _isDead = true;
        Stats.onDie -= Die;
    }

    protected virtual bool HasAttack()
    {
        float distance = Vector3.Distance(_player.transform.position,
           transform.position);

        return distance <= Stats.RadiusAttack.GetValue();
    }

    protected virtual bool HasFollow()
    {
        float distance = Vector3.Distance(_player.transform.position,
            transform.position);

        return distance >= Stats.RadiusFollow.GetValue();
    }

    protected abstract void InitializedFSM();
}

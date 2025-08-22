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
    protected NavMeshAgent _agent;

    public EnemyStats Stats { get; private set; }

    private void Awake()
    {
        _lootBag = GetComponent<LootBag>();
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        _fsm.Update();
    }

    protected virtual void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public virtual void TakeDamage(float damage)
    {
        Stats.TakeDamage(damage);
    }

    public virtual void Initialized(Player player, IEnemyObserver observer,
        CustomPool<Bullet> bulletPool)
    {
        _player = player;
        _observer = observer;
        _bulletPool = bulletPool;

        Stats = new EnemyStats(_config);
        Stats.onDie += Die;

        InitializedFSM();
    }

    protected virtual void Die()
    {
        _lootBag.CreateItems(_player);
        Destroy(gameObject);
        
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

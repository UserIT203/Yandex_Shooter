using System;
using UnityEngine;

[RequireComponent(typeof(LootBag))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected EnemyConfig _config;

    protected Player _player;
    protected LootBag _lootBag;
    protected EnemyStats _stats;
    protected IEnemyObserver _observer;

    public EnemyStats Stats => _stats;

    public event Action<Player, CustomPool<Bullet>> onInitialized;

    private void Awake()
    {
        _lootBag = GetComponent<LootBag>();
    }

    public virtual void Initialized(Player target, IEnemyObserver observer, 
        CustomPool<Bullet> bulletPool = null)
    {
        _player = target;
        _observer = observer;

        _stats = new EnemyStats(_config);
        onInitialized?.Invoke(target, bulletPool);

        _stats.onDie += Die;
    }

    protected virtual void Die()
    {
        _observer.OnEnemyDestroed();
        _lootBag.CreateItems(_player);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        _stats?.TakeDamage(damage);
    }
}

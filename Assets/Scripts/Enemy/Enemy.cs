using System;
using UnityEngine;

[RequireComponent(typeof(LootBag))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;

    private Player _player;
    private LootBag _lootBag;
    private EnemyStats _stats;
    private IEnemyObserver _observer;

    public EnemyStats Stats => _stats;

    public event Action<Player, CustomPool<Bullet>> onInitialized;

    private void Awake()
    {
        _lootBag = GetComponent<LootBag>();
    }

    public void Initialized(Player target, IEnemyObserver observer, 
        CustomPool<Bullet> bulletPool = null)
    {
        _player = target;
        _observer = observer;

        _stats = new EnemyStats(_config);
        onInitialized?.Invoke(target, bulletPool);

        _stats.onDie += Die;
    }

    private void Die()
    {
        _observer.OnEnemyDestroed();
        _lootBag.CreateItems(_player);
        Destroy(gameObject);
    }
}

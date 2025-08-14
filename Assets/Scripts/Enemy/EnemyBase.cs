using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LootBag))]
public abstract class EnemyBase: MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;

    private Player _player;
    private LootBag _lootBag;
    private IEnemyObserver _enemyObserver;

    public EnemyStats EnemyStats { get; private set; }
    
    public event Action<Player> onInitialized;

    private void Awake()
    {
        _lootBag = GetComponent<LootBag>();
    }

    public virtual void Initialized(Player target, IEnemyObserver observer)
    {
        _player = target;
        _enemyObserver = observer;

        EnemyStats = new EnemyStats(_config);

        onInitialized?.Invoke(target);

        EnemyStats.onDie += Die;
    }

    protected virtual void Die()
    {
        _enemyObserver.OnEnemyDestroed();
        _lootBag.CreateItems(_player);
        Destroy(gameObject);
    }
}

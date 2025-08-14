using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

[RequireComponent(typeof(Enemy))]
public class EnemyCombat : MonoBehaviour
{
    protected Player _player;
    protected Enemy _enemy;

    private float _timer;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.onInitialized += Initialized;
    }

    protected virtual void Initialized(Player player, CustomPool<Bullet> bulletPool = null)
    {
        _player = player;
    }

    private void Update()
    {
        if (HasAttack())
        {
            Attack();
            _timer = 0f;
        }
    }

    protected virtual bool HasAttack()
    {
        _timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position,
            _player.transform.position);

        bool hasPlayerInRange = 
            distanceToPlayer <= _enemy.Stats.RadiusAttack.GetValue();

        bool hasReadyToAttack =
            _timer >= _enemy.Stats.AttackDealy.GetValue();

        return hasPlayerInRange && hasReadyToAttack;
    }

    protected virtual void Attack()
    {
        Debug.Log("Enemy " + gameObject.name + " Attack Player");
        _player.Stats.TakeDamage(_enemy.Stats.Damage.GetValue());
    }
}

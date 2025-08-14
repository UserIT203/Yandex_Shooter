using System;
using UnityEngine;

public class CharacterStats
{
    public Stat MaxHealth => _config.MaxHealth;
    public Stat Damage => _config.Damage;
    public Stat Speed => _config.Speed;

    protected Config _config;
    protected float _currentHealth;

    public event Action<float> onTakeDamage;
    public event Action onDie;

    public CharacterStats(Config config)
    {
        _config = config;       
        _currentHealth = _config.MaxHealth.GetValue();
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        onTakeDamage?.Invoke(damage);

        if (_currentHealth <= 0)
        {
            onDie?.Invoke();
            return;
        }
    }

    public virtual void ApplyModifiers(Config config)
    {
        MaxHealth.AddModifier(_config.MaxHealth.GetValue());
        Damage.AddModifier(_config.Damage.GetValue());
        Speed.AddModifier(_config.Speed.GetValue());
    }

    public void ApplyModifierFromWeapon(WeaponConfig weaponConfig)
    {
        Damage.Reset();
        Damage.AddModifier(weaponConfig.Damage);
        Debug.Log("Player Damage " + Damage.GetValue());
    }
}

public class EnemyStats: CharacterStats
{
    public Stat RadiusAttack { get; }
    public Stat AttackDealy { get; }

    private EnemyConfig _enemyConfig;

    public EnemyStats(Config config) : base(config) 
    {
        _enemyConfig = config as EnemyConfig;

        RadiusAttack = _enemyConfig.RadiusAttack;
        AttackDealy = _enemyConfig.AttackDealy;
    }

    public override void TakeDamage(float damage) 
    {
        base.TakeDamage(damage);
    }
}

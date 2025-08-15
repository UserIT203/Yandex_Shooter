using System;
using UnityEngine;

public class CharacterStats
{
    public Stat MaxHealth { get; }
    public Stat Damage { get; }
    public Stat Speed { get; }

    protected Config _config;
    protected float _currentHealth;

    public event Action<float> onTakeDamage;
    public event Action onDie;

    public CharacterStats(Config config)
    {
        _config = config; 
        
        MaxHealth = new Stat(config.MaxHealth.GetValue(), config.MaxHealth.Modificator);
        Damage = new Stat(config.Damage.GetValue(), config.Damage.Modificator);
        Speed = new Stat(config.Speed.GetValue(), config.Speed.Modificator  );

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
        MaxHealth.AddModifier(config.MaxHealth.GetValue());
        Damage.AddModifier(config.Damage.GetValue());
        Speed.AddModifier(config.Speed.GetValue());
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

        RadiusAttack = new Stat(_enemyConfig.RadiusAttack.GetValue(), 
            _enemyConfig.RadiusAttack.Modificator);
        AttackDealy = new Stat(_enemyConfig.AttackDealy.GetValue(),
            _enemyConfig.AttackDealy.Modificator);
    }

    public override void TakeDamage(float damage) 
    {
        base.TakeDamage(damage);
    }
}

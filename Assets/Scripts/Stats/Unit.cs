using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Unit : MonoBehaviour, IDamagable
{
    public Stat MaxHealth { get; protected set; }
    public Stat Damage { get; protected set; }
    public Stat Speed { get; protected set; }

    public float CurrentHealth { get; protected set; }

    public event Action<float> onTakeDamage;
    public event Action onDie;

    public virtual void SetConfig(Config config)
    {
        MaxHealth = new Stat(config.MaxHealth.GetValue(), config.MaxHealth.Modificator);
        Damage = new Stat(config.Damage.GetValue(), config.Damage.Modificator);
        Speed = new Stat(config.Speed.GetValue(), config.Speed.Modificator);

        CurrentHealth = config.MaxHealth.GetValue();
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        onTakeDamage?.Invoke(damage);

        if (CurrentHealth <= 0)
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

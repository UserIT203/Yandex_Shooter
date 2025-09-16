using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerStats : CharacterStats, ITickable
{
    private const float RebornInvulnerableTime = 2f;

    private PlayerConfig _playerConfig;

    public Stat ItemPickUpRadius { get; }

    public  IUltimate Ultimate { get; }

    private bool _isInvulnerable = false;
    private bool _canRebornEffect = false;
    private float _rebornEffectTimer = 0f;

    public PlayerStats(Config config) : base(config)
    {
        _playerConfig = config as PlayerConfig;
        ItemPickUpRadius = new Stat(_playerConfig.PickUpRadius.GetValue(), 
            _playerConfig.PickUpRadius.Modificator);
        Ultimate = _playerConfig.Ultimate;
    }

    public override void TakeDamage(float damage)
    {
        if (_isInvulnerable) return;

        base.TakeDamage(damage);
        Debug.Log("Take damage Player " + damage);
    }

    public void SetInvulnerableStatus(bool isInvulnerable) => _isInvulnerable = isInvulnerable;

    public void Heal(float health)
    {
        if(CurrentHealth >= MaxHealth.GetValue()) return;

        float oldHealth = CurrentHealth;
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, MaxHealth.GetValue());
        
        onChangeHealth?.Invoke(CurrentHealth, MaxHealth.GetValue());
    }

    public void Reborn()
    {
        Heal(MaxHealth.GetValue());
        _canRebornEffect = true;
    }

    public void Tick()
    {
        if (_canRebornEffect == false) return;

        _rebornEffectTimer += Time.deltaTime;
        _isInvulnerable = true;

        if(_rebornEffectTimer >= RebornInvulnerableTime)
        {
            _isInvulnerable = false;
            _canRebornEffect = false;
            _rebornEffectTimer = 0f;
        }
    }
}

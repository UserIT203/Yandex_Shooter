using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerConfig _playerConfig;

    public Stat ItemPickUpRadius { get; }

    public  IUltimate Ultimate { get; }

    private bool _isInvulnerable = false;

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
}

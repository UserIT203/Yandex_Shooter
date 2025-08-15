using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerConfig _playerConfig;

    public Stat ItemPickUpRadius { get; }

    public PlayerStats(Config config) : base(config)
    {
        _playerConfig = config as PlayerConfig;
        ItemPickUpRadius = new Stat(_playerConfig.PickUpRadius.GetValue(), 
            _playerConfig.PickUpRadius.Modificator);
    }
}

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
        ItemPickUpRadius = _playerConfig.PickUpRadius;
    }
}

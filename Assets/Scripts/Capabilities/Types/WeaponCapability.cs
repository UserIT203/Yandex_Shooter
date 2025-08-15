using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCapability : CapabilitieBase
{
    private PlayerCombat _playerCombat;

    public WeaponCapability(List<CapabilitieConfig> upgrades, CapabilitieConfig defaultConfig, Player player) : base(upgrades, defaultConfig, player)
    {
        _playerCombat = player.GetComponent<PlayerCombat>();
        Unlock();
    }

    public override void Activate(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    protected override void SetUpgrade(CapabilitieConfig config)
    {
        _playerCombat.SetWeapon(Upgrades[Level - 1].WeaponConfig);
    }
}

   

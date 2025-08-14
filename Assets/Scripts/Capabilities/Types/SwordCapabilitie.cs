using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCapabilitie : CapabilitieBase
{
    private PlayerCapabilities _playerCapabilite;

    public SwordCapabilitie(List<CapabilitieConfig> upgrades, CapabilitieConfig defaultConfig, Player player) : base(upgrades, defaultConfig, player)
    {
        _playerCapabilite = player.GetComponent<PlayerCapabilities>();
    }

    public override void Unlock()
    {
        base.Unlock();
        _playerCapabilite.UnlockSwords();
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetUpgrade(CapabilitieConfig config)
    {
        _playerCapabilite.UpgradeSword(config);
    }
}

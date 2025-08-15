using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CapabilitieBase: ICapabilitie
{
    public bool IsUnlock { get; protected set; }

    public int Level { get; protected set; }

    public Player Player { get; protected set; }

    public List<CapabilitieConfig> Upgrades { get; protected set; }

    public CapabilitieConfig DefaultConfig { get; protected set; }

    public CapabilitieBase(List<CapabilitieConfig> upgrades,
        CapabilitieConfig defaultConfig, Player player)
    {
        IsUnlock = false;
        Level = 0;
        Upgrades = upgrades;
        DefaultConfig = defaultConfig;
        Player = player;
    }

    public abstract void Activate(Vector3 direction);

    public virtual void Unlock()
    {
        if (IsUnlock) return;

        IsUnlock = true;
        Level = 1;
    }

    public bool TryUpgrade()
    {
        if (IsUnlock == false || Level >= Upgrades.Count) return false;

        Level++;

        SetUpgrade(Upgrades[Level - 1]);

        return true;
    }

    public CapabilitieConfig GetCurrentUpgradeConfig()
    {
        return Level == Upgrades.Count ? Upgrades[Level - 1] : Upgrades[Level];
    }

    protected abstract void SetUpgrade(CapabilitieConfig config);

    public bool IsMaxLevel() => Level >= Upgrades.Count;
}

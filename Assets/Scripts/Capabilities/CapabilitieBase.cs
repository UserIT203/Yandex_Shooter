using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CapabilitieBase: ICapabilitie
{
    public bool IsUnlock { get; protected set; }

    public int Level { get; protected set; }

    public Player Player { get; protected set; }

    public List<CapabilitieConfig> Upgrades { get; protected set; }

    public CapabilitieConfig DefaultConfig { get; protected set; }

    public event Action onUpgrade;
    public event Action onUnlock;
    public event Action<float, float> onCapabilitieTimer;

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
       
        onUnlock?.Invoke();
    }

    public bool TryUpgrade()
    {
        if (IsUnlock == false || Level >= Upgrades.Count) return false;

        Level++;

        SetUpgrade(Upgrades[Level - 1]);
        onUpgrade?.Invoke();

        return true;
    }

    public CapabilitieConfig GetCurrentUpgradeConfig()
    {
        int index = Mathf.Clamp(Level, 0, Upgrades.Count - 1);
        return Upgrades[index];
    }

    protected abstract void SetUpgrade(CapabilitieConfig config);

    protected virtual void OnCapabilitieTimer(float currentTime, float maxTime)
    {
        onCapabilitieTimer?.Invoke(currentTime, maxTime);
    }

    public bool IsMaxLevel() => Level >= Upgrades.Count;
}

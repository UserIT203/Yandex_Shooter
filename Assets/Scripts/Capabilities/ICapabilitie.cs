using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICapabilitie
{
    List<CapabilitieConfig> Upgrades { get; }
    bool IsUnlock { get; }
    int Level { get; }

    public event Action onUpgrade;
    public event Action onUnlock;
    public event Action<float, float> onCapabilitieTimer;

    void Unlock();
    bool TryUpgrade();
    void Activate(Vector3 direction);
    CapabilitieConfig GetCurrentUpgradeConfig();
    bool IsMaxLevel();
}

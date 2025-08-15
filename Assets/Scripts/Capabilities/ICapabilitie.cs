using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICapabilitie
{
    List<CapabilitieConfig> Upgrades { get; }
    bool IsUnlock { get; }
    int Level { get; }

    void Unlock();
    bool TryUpgrade();
    void Activate(Vector3 direction);
    CapabilitieConfig GetCurrentUpgradeConfig();
    bool IsMaxLevel();
}

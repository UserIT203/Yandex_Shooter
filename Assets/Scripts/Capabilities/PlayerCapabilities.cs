using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerCapabilities : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<CapabiliteUpgradeLevel> _capabilitiUpgrades;
    [Header("Sword Around Options")]
    [SerializeField] private List<Sword> _swords;

    private Dictionary<CapabilitiesType, ICapabilitie> _capabilities = new Dictionary<CapabilitiesType, ICapabilitie>();

    private int _maxSwordsCount;
    private int _currentSwordsCount;

    private void Awake()
    {
        _maxSwordsCount = _swords.Count;

        _capabilities.Add(_capabilitiUpgrades[0].CapabilitieType,
                new WeaponCapability(_capabilitiUpgrades[0].CapabilitieConfigs, 
                _capabilitiUpgrades[0].DefaultConfig,
                GetComponent<Player>()));

        _capabilities.Add(_capabilitiUpgrades[1].CapabilitieType,
            new SwordCapabilitie(_capabilitiUpgrades[1].CapabilitieConfigs,
                _capabilitiUpgrades[1].DefaultConfig,
                GetComponent<Player>()));
    }

    public ICapabilitie GetCapabilite(CapabilitiesType type)
    {
        return _capabilities[type];
    }

    #region Sword
    public void UnlockSwords()
    {
        CapabilitieConfig config = GetCapabilite(CapabilitiesType.SwordAround).GetCurrentUpgradeConfig();

        foreach (Sword sword in _swords)
        {
            if(sword.gameObject.activeSelf == false)
            {
                sword.UnlockSword();
                sword.UpdateOptions(config.Damage, config.Speed);
                break;
            }
        }
    }

    public void UpgradeSword(CapabilitieConfig config)
    {
        foreach (Sword sword in _swords)
            sword.UpdateOptions(config.Damage, config.Speed);

        if (config.CanAddOne)
            UnlockSwords();
    }
    #endregion
}

[System.Serializable]
public class CapabiliteUpgradeLevel
{
    public CapabilitiesType CapabilitieType;
    public CapabilitieConfig DefaultConfig;
    public List<CapabilitieConfig> CapabilitieConfigs;
}

public enum CapabilitiesType
{
    Weapon,
    SwordAround
}

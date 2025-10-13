using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerCapabilities : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<CapabiliteUpgradeLevel> _capabilitiUpgrades;
    [Header("Sword Around Options")]
    [SerializeField] private List<Sword> _swords;

    private Dictionary<CapabilitiesType, ICapabilitie> _capabilities = new Dictionary<CapabilitiesType, ICapabilitie>();

    [Inject]
    public void Construct(WaveManager waveManager)
    {
        waveManager.onStartWave += CreateTurret;
    }

    private void Awake()
    {
        _capabilities.Add(_capabilitiUpgrades[0].CapabilitieType,
               new WeaponCapability(_capabilitiUpgrades[0].CapabilitieConfigs,
               _capabilitiUpgrades[0].DefaultConfig,
               GetComponent<Player>()));

        _capabilities.Add(_capabilitiUpgrades[1].CapabilitieType,
            new SwordCapabilitie(_capabilitiUpgrades[1].CapabilitieConfigs,
                _capabilitiUpgrades[1].DefaultConfig,
                GetComponent<Player>()));

        _capabilities.Add(_capabilitiUpgrades[2].CapabilitieType,
           new DashCapabilitie(_capabilitiUpgrades[2].CapabilitieConfigs,
               _capabilitiUpgrades[2].DefaultConfig,
               GetComponent<Player>()));

        _capabilities.Add(_capabilitiUpgrades[3].CapabilitieType,
           new TurretCapability(_capabilitiUpgrades[3].CapabilitieConfigs,
               _capabilitiUpgrades[3].DefaultConfig,
               GetComponent<Player>()));
    }

    public ICapabilitie GetCapabilite(CapabilitiesType type)
    {
        return _capabilities[type];
    }

    #region Turret

    private void CreateTurret(int a)
    {
        GetCapabilite(CapabilitiesType.Turret).Activate(Vector3.zero);
    }

    #endregion

    #region Sword

    private void ArrangeSwords()
    {
        int activeSwordsCount = 0;

        foreach (Sword sword in _swords)
        {
            if (sword.gameObject.activeSelf)
                activeSwordsCount++;
        }

        if (activeSwordsCount == 0) return;

        float angleStep = 360f / activeSwordsCount;
        float currentAngle = 0f;

        foreach (Sword sword in _swords)
        {
            if (!sword.gameObject.activeSelf) continue;

            Vector3 offset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * sword.GetRadius();
            Vector3 newPosition = transform.position + offset;
            newPosition.y = sword.transform.position.y;
            sword.transform.position = newPosition;

            Vector3 direction = (newPosition - transform.position).normalized;
            sword.transform.rotation = Quaternion.LookRotation(direction);
            sword.transform.Rotate(90, 90, 180);

            currentAngle += angleStep;
        }
    }

    public void UnlockSwords()
    {
        CapabilitieConfig config = GetCapabilite(CapabilitiesType.SwordAround).GetCurrentUpgradeConfig();

        foreach (Sword sword in _swords)
        {
            sword.UpdateOptions(config.Damage, config.Speed);

            if(sword.gameObject.activeSelf == false)
            {
                sword.UnlockSword();
                sword.UpdateOptions(config.Damage, config.Speed);
                ArrangeSwords();
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
    SwordAround,
    Dash,
    Turret
}

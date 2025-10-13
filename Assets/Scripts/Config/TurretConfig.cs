using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Turret Capabilitie")]
public class TurretConfig : CapabilitieConfig
{
    [field: SerializeField] public float AttackRadius { get; private set; }
    [field: SerializeField] public float SpawnRadius { get; private set; }
    [field: SerializeField] public float LifeTime { get; private set; }
    [field: SerializeField] public int TurretCount { get; private set; }
    [field: SerializeField] public Turret TurretPrefab { get; private set; }
}

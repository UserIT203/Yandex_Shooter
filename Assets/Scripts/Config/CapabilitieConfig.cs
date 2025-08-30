using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Capabilitie")]
public class CapabilitieConfig : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Delay {  get; private set; }
    [field: SerializeField] public float Speed {  get; private set; }
    [field: SerializeField] public bool CanAddOne { get; private set; }
    [field: SerializeField] public WeaponConfig WeaponConfig {  get; private set; }
}

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Turret Capabilitie")]
public class TurretConfig : CapabilitieConfig
{
    [field: SerializeField] public float AttackRadius { get; private set; }
    [field: SerializeField] public float SpawnRadius { get; private set; }
    [field: SerializeField] public int TurretCount { get; private set; }
    [field: SerializeField] public Turret TurretPrefab { get; private set; }
}
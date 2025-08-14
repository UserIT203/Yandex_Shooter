using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/WeaponConfig")]
public class WeaponConfig: ScriptableObject
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField, Range(1, 3)] public int BulletShootingCount;
    [field: SerializeField] public int BulletCount { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public Bullet BulletType;
}

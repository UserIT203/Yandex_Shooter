using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName ="Configs")]
public class Config : ScriptableObject
{
    [field: SerializeField] public Stat Speed { get; private set; }
    [field: SerializeField] public Stat MaxHealth { get; private set; }
    [field: SerializeField] public Stat Damage { get; private set; }
}

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Enemy")]
public class EnemyConfig: Config
{
    [field: SerializeField] public Stat RadiusAttack { get; private set; }
    [field: SerializeField] public Stat AttackDealy { get; private set; }
}

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Player")]
public class PlayerConfig : Config
{
    [field: SerializeField] public Stat PickUpRadius { get; private set; }
    [field: SerializeField] public UltimateBase Ultimate { get; private set; }
}

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Boss")]
public class BossConfig : EnemyConfig
{
    [field: SerializeField] public string Ultimait { get; private set; }
}
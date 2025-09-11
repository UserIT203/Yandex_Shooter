using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Enemy")]
public class EnemyConfig : Config
{
    [field: SerializeField] public Stat RadiusAttack { get; private set; }
    [field: SerializeField] public Stat RadiusFollow { get; private set; }
    [field: SerializeField] public Stat AttackDealy { get; private set; }
}

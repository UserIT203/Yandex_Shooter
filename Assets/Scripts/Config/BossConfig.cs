using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Boss")]
public class BossConfig : EnemyConfig
{
    [field: SerializeField] public UltimateBase Ultimate { get; private set; }
}

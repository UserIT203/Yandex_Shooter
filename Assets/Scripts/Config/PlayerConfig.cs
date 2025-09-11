using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Player")]
public class PlayerConfig : Config
{
    [field: SerializeField] public Stat PickUpRadius { get; private set; }
    [field: SerializeField] public UltimateBase Ultimate { get; private set; }
}

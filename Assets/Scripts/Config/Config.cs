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
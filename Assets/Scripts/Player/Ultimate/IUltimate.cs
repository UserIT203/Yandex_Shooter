using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUltimate
{
    public string UltimateName { get; }
    public float UltimateDuration { get; }
    public float Cooldown { get; }
    public void Initialized(Player player);
    public void Update();
    public void TryUse();
}

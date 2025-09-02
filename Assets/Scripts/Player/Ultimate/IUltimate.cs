using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUltimate
{
    public Image UltimateIcon { get; }
    public string UltimateName { get; }
    public float UltimateDuration { get; }
    public float Cooldown { get; }
    public bool IsStarted { get; }

    public event Action onUltimateEnd;

    public void Initialized(Player player);
    public void Update();
    public void TryUse();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootingSystem
{
    public event Action<Vector3> onShoot;
    public event Action onReload;
    public float Range { get; }
    public void Shoot();
    public bool CanShoot();
    public void Reload();
    public void HandleShooting();
}

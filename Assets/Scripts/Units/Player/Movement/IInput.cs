using System;
using UnityEngine;

public interface IInput
{
    public event Action<Vector3> onDash;
    public event Action<Vector3> onMove;
    public void UpdateInput();
    public void DashInput();
}

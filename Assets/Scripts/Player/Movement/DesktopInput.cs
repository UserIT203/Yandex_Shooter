using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : IInput
{
    public event Action<Vector3> onMove;

    public void UpdateInput()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 targetVelocity = new Vector3(horizontal, 0, vertical).normalized;

        onMove?.Invoke(targetVelocity);
    }
}

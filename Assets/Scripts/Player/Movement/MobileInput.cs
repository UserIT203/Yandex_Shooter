using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : IInput
{
    public event Action<Vector3> onMove;

    private Joystick _joystick;

    public void UpdateInput()
    {
        Move();
    }

    public MobileInput(Joystick joystick)
    {
        _joystick = joystick;
        Debug.Log("Джостик " + _joystick.name);
    }

    private void Move()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

        Vector3 targetVelocity = new Vector3(horizontal, 0, vertical).normalized;

        onMove?.Invoke(targetVelocity);
    }
}

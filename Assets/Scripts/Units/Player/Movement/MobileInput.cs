using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : IInput, IDisposable
{
    public event Action<Vector3> onMove;
    public event Action<Vector3> onDash;

    private Joystick _joystick;
    private Button _dashButton;

    public MobileInput(Joystick joystick, Button dashButton)
    {
        _joystick = joystick;
        _dashButton = dashButton;

        _dashButton.onClick.AddListener(DashInput);

        Debug.Log("Button " + dashButton.name);
        Debug.Log("Джостик " + _joystick.name);
    }

    public void Dispose()
    {
        _dashButton.onClick.RemoveListener(DashInput);
        Debug.Log("MobileInput disposed");
    }

    public void UpdateInput()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

        Vector3 targetVelocity = new Vector3(horizontal, 0, vertical).normalized;

        onMove?.Invoke(targetVelocity);
    }

    public void DashInput()
    {
        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 dashDirection = new Vector3(
                _joystick.Horizontal,
                0f,
                _joystick.Vertical);

            onDash?.Invoke(dashDirection);
        }
    }
}

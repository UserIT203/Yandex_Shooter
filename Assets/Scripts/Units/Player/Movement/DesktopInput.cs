using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : IInput
{
    private const KeyCode DashKeyCode = KeyCode.LeftShift;

    public event Action<Vector3> onMove;
    public event Action<Vector3> onDash;

    public void DashInput()
    {
        if(Input.GetKeyDown(DashKeyCode) && (Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0))
        {
            Vector3 dashDirection = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0f,
                Input.GetAxisRaw("Vertical"));

            onDash?.Invoke(dashDirection);
        }
    }

    public void UpdateInput()
    {
        Move();
        DashInput();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 targetVelocity = new Vector3(horizontal, 0, vertical).normalized;

        onMove?.Invoke(targetVelocity);
    }
}

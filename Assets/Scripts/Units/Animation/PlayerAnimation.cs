using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : UnitAnimation
{
    private CharacterController _controller;

    public override void DieAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialized()
    {
        base.Initialized();
        _controller = transform.root.GetComponent<CharacterController>();
    }

    protected override void SetUnitSpeed()
    {
        MoveAnimation(_controller.velocity.magnitude);
    }
}

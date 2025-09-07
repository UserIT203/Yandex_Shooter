using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class UnitAnimation : MonoBehaviour
{
    protected Animator _animator;

    private void Awake()
    {
        Initialized();
    }

    private void Update()
    {
        SetUnitSpeed();
    }

    public abstract void DieAction();

    protected abstract void SetUnitSpeed();

    protected virtual void Initialized()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void MoveAnimation(float speed)
    {
        _animator.SetFloat("unitSpeed", speed);
    }

    protected virtual void DieAnimation() { }

    protected virtual void AttackAnimation() { }
}

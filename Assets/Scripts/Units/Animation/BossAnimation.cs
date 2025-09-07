using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : EnemyAnimation 
{
    private Boss _boss;

    private void OnDisable()
    {
        _boss.onUltimate -= UltimateAnimation;
    }

    protected override void Initialized()
    {
        base.Initialized();
        _boss = _enemyUnit as Boss;

        _boss.onUltimate += UltimateAnimation;
    }

    private void UltimateAnimation(bool status)
    {
        _animator.SetBool("isUltimate", status);
    }
}

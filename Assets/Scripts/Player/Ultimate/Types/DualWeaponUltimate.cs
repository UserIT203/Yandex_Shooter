using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DualWeapon", menuName = "Player Ultimate/DualWeapon")]
public class DualWeaponUltimate : UltimateBase, IDisposable
{
    private PlayerCombat _playerCombat;
    private float _modificatorBonus;

    public void Dispose()
    {
        _playerCombat.onSetWeapon -= SetModificator;
    }

    public override void Initialized(Player player)
    {
        base.Initialized(player);
        _playerCombat = player.GetComponent<PlayerCombat>();
        _playerCombat.onSetWeapon += SetModificator;
    }

    protected override void Execute()
    {
        base.Execute();
        SetModificator();
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        _player.Stats.Damage.RemodeModifier(_modificatorBonus);
        _modificatorBonus = 0;
        Debug.Log("[END] Player Damage " + _player.Stats.Damage.GetValue());
    }

    private void SetModificator()
    {
        if(IsStarted == true)
        {
            _player.Stats.Damage.RemodeModifier(_modificatorBonus);
            _modificatorBonus = _player.Stats.Damage.GetValue();
            _player.Stats.Damage.AddModifier(_modificatorBonus);
            Debug.Log("[Start] Player Damage " + _player.Stats.Damage.GetValue());
        }
    }
}

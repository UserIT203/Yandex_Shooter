using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesktopWeaponHUD : MonoBehaviour, IWeaponHUD
{
    private const string BulletLabelText = "{0}/{1}";

    [field: SerializeField] public Image FillImageBullet { get; protected set; }
    [field: SerializeField] public Image FillImageUltimate { get; protected set; }
    [field: SerializeField] public Image IconUltimate { get; protected set; }
    [field: SerializeField] public TMP_Text BulletLabel { get; protected set; }


    private WeaponBase _weapon;

    private void OnDisable()
    {
        _weapon.onBulletInMagazine -= ChangeBulletCount;
    }

    public void ChangeBulletCount(int currentCount, int totalCount)
    {
        BulletLabel.text = string.Format(BulletLabelText, currentCount, totalCount);
        
    }

    public void SetUltimate(UltimateBase ultimate)
    {
        throw new System.NotImplementedException();
    }

    public void SetWeapon(WeaponBase weapon)
    {
        Debug.Log("Set weapon " + weapon);

        if (_weapon == null)
        {
            _weapon = weapon;
            _weapon.onBulletInMagazine += ChangeBulletCount;
        }

        ChangeBulletCount(weapon.CurrentBulletInMagazine, weapon.CurrentBulletInMagazine);
    }
}

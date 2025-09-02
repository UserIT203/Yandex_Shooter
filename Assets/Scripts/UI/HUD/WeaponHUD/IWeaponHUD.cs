using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IWeaponHUD 
{
    public Image FillImageBullet { get; }
    public Image FillImageUltimate { get; }
    public Image IconUltimate { get; }
    public TMP_Text BulletLabel { get; }

    public void SetUltimate(UltimateBase ultimate);
    public void SetWeapon(WeaponBase weapon);
    public void ChangeBulletCount(int currentCount, int totalCount);
}

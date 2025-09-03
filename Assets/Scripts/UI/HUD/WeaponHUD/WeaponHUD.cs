using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class WeaponHUD : MonoBehaviour, IWeaponHUD
{
    private const string BulletLabelText = "{0}/{1}";

    [field: SerializeField] public Image FillImageBullet { get; protected set; }
    [field: SerializeField] public Image FillImageUltimate { get; protected set; }
    [field: SerializeField] public Image IconUltimate { get; protected set; }
    [field: SerializeField] public TMP_Text BulletLabel { get; protected set; }


    private WeaponBase _weapon;
    private IUltimate _ultimate;
    private float _ultimateTimer;

    private void OnDisable()
    {
        _weapon.onBulletInMagazine -= PlayChangeBulletAnimation;
        _ultimate.onUltimateTimer -= ChangeUltimateFillImage;
    }

    public void PlayChangeBulletAnimation(int currentCount, int totalCount)
    {
        BulletLabel.text = string.Format(BulletLabelText, currentCount, totalCount);
        ChangeBulletFillImage(currentCount, totalCount);
    }

    public void SetUltimate(IUltimate ultimate)
    {
        _ultimate = ultimate;
        IconUltimate.sprite = _ultimate.UltimateIcon;

        _ultimateTimer = _ultimate.Cooldown;
        _ultimate.onUltimateTimer += ChangeUltimateFillImage;
    }

    public void SetWeapon(WeaponBase weapon)
    {
        if (_weapon == null)
        {
            _weapon = weapon;
            _weapon.onBulletInMagazine += PlayChangeBulletAnimation;
        }

        PlayChangeBulletAnimation(weapon.CurrentBulletInMagazine, weapon.CurrentBulletInMagazine);
    }

    private void ChangeBulletFillImage(int currentBullet, int totalBullet)
    {
        float value = (float)currentBullet / (float)totalBullet;
        FillImageBullet.DOFillAmount(value, 0.5f).SetEase(Ease.OutQuad);
    }

    private void ChangeUltimateFillImage(float timer)
    {
        if (timer < 0) return;

        float value = timer / _ultimateTimer;
        FillImageUltimate.DOFillAmount(value, 0.5f).SetEase(Ease.OutQuad);
    }
}

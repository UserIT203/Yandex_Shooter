using DG.Tweening;
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
    [field: SerializeField] public Image ReloadIcon { get; protected set; }
    [field: SerializeField] public TMP_Text BulletLabel { get; protected set; }

    private WeaponBase _weapon;
    private IUltimate _ultimate;
    private float _ultimateTimer;
    private Sequence _reloadSequence;

    private void OnDisable()
    {
        _weapon.onBulletInMagazine -= PlayChangeBulletAnimation;
        _ultimate.onUltimateTimer -= ChangeUltimateFillImage;
        _weapon.onReloading -= ReloadImage;
    }

    private void Awake()
    {
        ReloadIcon.gameObject.SetActive(false);
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
            _weapon.onReloading += ReloadImage;
        }

        PlayChangeBulletAnimation(weapon.CurrentBulletInMagazine, weapon.CurrentBulletInMagazine);
    }

    private void ChangeBulletFillImage(int currentBullet, int totalBullet)
    {
        Debug.Log(currentBullet + " | " + totalBullet);
        float value = (float)currentBullet / (float)totalBullet;
        FillImageBullet.DOFillAmount(value, 0.5f).SetEase(Ease.OutQuad);
    }

    private void ChangeUltimateFillImage(float timer)
    {
        if (timer < 0) return;

        float value = timer / _ultimateTimer;
        FillImageUltimate.DOFillAmount(value, 0.5f).SetEase(Ease.OutQuad);
    }

    private void ReloadImage(bool state)
    {
        if (state == true)
        {
            ReloadIcon.gameObject.SetActive(true);
            PlayReloadAnimation();
        }   
        else
        {
            ReloadIcon.gameObject.SetActive(false);
            _reloadSequence?.Kill();
        }
    }

    private void PlayReloadAnimation()
    {
        _reloadSequence = DOTween.Sequence();

        _reloadSequence.Append(
            ReloadIcon.rectTransform.DORotate(
                new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)).
                SetLoops(-1, LoopType.Restart);
    }
}

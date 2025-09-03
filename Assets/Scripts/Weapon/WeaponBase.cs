using System;
using System.Collections;
using UnityEngine;

public class WeaponBase
{
    private WeaponConfig _weaponConfig;
    private int _currentBulletInMagazine;
    private bool _isReloading;
    private MonoBehaviour _coroutineRunner;
    private float _lastShootTime;

    public int CurrentBulletInMagazine => _currentBulletInMagazine;

    public event Action<int> onShoot;
    public event Action<int, int> onBulletInMagazine;

    public WeaponBase(WeaponConfig config, MonoBehaviour coroutineRunner)
    {
        SetWeapon(config);
        _coroutineRunner = coroutineRunner;
    }

    public void SetWeapon(WeaponConfig config)
    {
        _weaponConfig = config;
        _currentBulletInMagazine = _weaponConfig.BulletCount;
    }

    public bool TryShoot()
    {
        if (_isReloading || Time.time < _lastShootTime + _weaponConfig.FireRate)
            return false;

        if (_currentBulletInMagazine <= 0)
        {
            Reload();
            return false;
        }

        _currentBulletInMagazine -= _weaponConfig.BulletShootingCount;
        _lastShootTime = Time.time;

        if (_currentBulletInMagazine <= 0)
            Reload();

        onShoot?.Invoke(_weaponConfig.BulletShootingCount);
        onBulletInMagazine?.Invoke(_currentBulletInMagazine, _weaponConfig.BulletCount);

        return true;
    }

    public void Reload()
    {
        if (_isReloading == false && _currentBulletInMagazine < _weaponConfig.BulletCount)
            _coroutineRunner.StartCoroutine(ReloadCorotine());
    }

    private IEnumerator ReloadCorotine()
    {
        Debug.Log("Перезарядка");
        _isReloading = true;

        yield return new WaitForSeconds(_weaponConfig.ReloadTime);

        _currentBulletInMagazine = _weaponConfig.BulletCount;
        _isReloading = false;
        onBulletInMagazine?.Invoke(_currentBulletInMagazine, _currentBulletInMagazine);
    }
}


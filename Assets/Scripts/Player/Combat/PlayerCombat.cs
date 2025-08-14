using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Player))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _bulletPoolContainer;
    [SerializeField] private WeaponConfig _defaultWeaponConfig;

    private WeaponConfig _currentWeaponConfig;
    private Player _player;
    private CustomPool<Bullet> _bulletPool;
    private WeaponBase _weapon;
    private LayerMask _shootMask;
    private IShootingSystem _shootingSystem;
    private Vector3 _shootDirection;

    public LayerMask ShootMask => _shootMask;
    public Transform FirePoint => _firePoint;

    [Inject]
    public void Construct(IShootingSystem shootingSystem, LayerMask mask)
    {
        _shootMask = mask;

        _shootingSystem = shootingSystem;
        _shootingSystem.onShoot += Shoot;
        _shootingSystem.onReload += Reload;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();

        if (_currentWeaponConfig == null) {
            _weapon = new WeaponBase(_defaultWeaponConfig, this);
            SetWeapon(_defaultWeaponConfig);
        }

        _weapon.onShoot += CreateBullet;

        _bulletPool = new CustomPool<Bullet>(_currentWeaponConfig.BulletType,
            _currentWeaponConfig.BulletCount, _bulletPoolContainer);
    }

    private void Update()
    {
        _shootingSystem.HandleShooting();
    }

    private void Shoot(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        _shootDirection = direction;
        _weapon.TryShoot();
    }
    

    private void CreateBullet(int bulletCount)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = _firePoint.transform.position;
        bullet.Shoot(_shootDirection, _bulletPool, _player.Stats.Damage.GetValue());
    }

    private void Reload()
    {
        _weapon.Reload();
    }

    public void SetWeapon(WeaponConfig weaponConfig)
    {
        _currentWeaponConfig = weaponConfig;
        _player.Stats.ApplyModifierFromWeapon(weaponConfig);
        _weapon.SetWeapon(weaponConfig);
    }
}
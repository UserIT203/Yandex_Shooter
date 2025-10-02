using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCapability : CapabilitieBase
{
    private float _damage, _attackDelay, _attackRadius, _spawnRadius;
    private int _turretCount;

    private CustomPool<Bullet> _bulletPool;
    private Turret _turretPrefab;

    public TurretCapability(List<CapabilitieConfig> upgrades, CapabilitieConfig defaultConfig, Player player) : base(upgrades, defaultConfig, player)
    {       
        _bulletPool = Player.GetComponent<PlayerCombat>().BulletPool;
        Unlock();
    }

    public override void Activate(Vector3 direction)
    {
        Debug.Log("Create Turret " + _turretCount);

        for (int i = 0; i < _turretCount; i++)
        {
            Turret turret = Player.Instantiate(
                _turretPrefab, 
                GetRandomPosition(), 
                Quaternion.identity);

            turret.Initialized(_damage, _attackRadius, _attackDelay, _bulletPool);
        }
    }

    protected override void SetUpgrade(CapabilitieConfig config)
    {
        TurretConfig turretConfig = config as TurretConfig;

        _damage = turretConfig.Damage;
        _attackDelay = turretConfig.Delay;
        _attackRadius = turretConfig.AttackRadius;
        _spawnRadius = turretConfig.SpawnRadius;

        _turretCount = turretConfig.TurretCount;
        _turretPrefab = turretConfig.TurretPrefab;
    }

    public override void Unlock()
    {
        base.Unlock();
        SetUpgrade(DefaultConfig);
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
        Vector3 position = Player.transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
        position.y = 0f;

        return position;
    }
}

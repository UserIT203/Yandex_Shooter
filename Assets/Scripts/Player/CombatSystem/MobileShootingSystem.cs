using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MobileShootingSystem : IShootingSystem
{
    public float Range => 5f;

    private LayerMask _mask;
    private PlayerCombat _player;
    private Collider[] _enemiesInRange;
    private Button _reloadButton;

    public event Action<Vector3> onShoot;
    public event Action onReload;

    public MobileShootingSystem(Player player, Button button, LayerMask mask)
    {
        _player = player.GetComponent<PlayerCombat>();
        _reloadButton = button;
        _mask = mask;
    }

    public void HandleShooting()
    {
        if (CanShoot())
            Shoot();

        Reload();
    }

    public bool CanShoot()
    {
        return HasTargetInRange();
    }

    public void Shoot()
    {
        Debug.Log("Mobile Shoot");

        Collider nearestEnemy = GetNearestEnemy();

        if (nearestEnemy != null)
        {
            Vector3 shootDirection =
                (nearestEnemy.transform.position - _player.FirePoint.position).normalized;
            shootDirection.y = 0;

            onShoot?.Invoke(shootDirection);
        }

        onShoot?.Invoke(Vector3.zero);
    }

    private bool HasTargetInRange()
    {
        _enemiesInRange = Physics.OverlapSphere(_player.transform.position,
            Range, _mask);

        return _enemiesInRange.Length > 0;
    }

    private Collider GetNearestEnemy()
    {
        if (_enemiesInRange.Length == 0) return null;

        Collider nearestEnemy = _enemiesInRange[0];
        float nearestSqrDistance = (_player.transform.position - nearestEnemy.transform.position).sqrMagnitude;

        for (int i = 1; i < _enemiesInRange.Length; i++)
        {
            float currentSqrDistance = (_player.transform.position - _enemiesInRange[i].transform.position).sqrMagnitude;
            if (currentSqrDistance < nearestSqrDistance)
            {
                nearestSqrDistance = currentSqrDistance;
                nearestEnemy = _enemiesInRange[i];
            }
        }

        return nearestEnemy;
    }


    public void Reload()
    {
        _reloadButton.onClick.AddListener(() => onReload?.Invoke());
    }
}

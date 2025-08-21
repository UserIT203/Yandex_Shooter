using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Zenject.SpaceFighter;

public class PCShootingSystem : IShootingSystem
{
    public float Range => 5f;

    private LayerMask _layerMask;
    private Camera _camera;
    private PlayerCombat _player;

    public event Action<Vector3> onShoot;
    public event Action onReload;
    public event Action onUseUltimate;

    public PCShootingSystem(Player player, LayerMask mask)
    {
        _camera = Camera.main;
        _player = player.GetComponent<PlayerCombat>();
        _layerMask = mask;
    }

    public void HandleShooting()
    {
        if (CanShoot())
            Shoot();

        HandleUlitimate();
        Reload();
    }

    public bool CanShoot()
    {
        return Input.GetMouseButton(0);
    }

    public void Shoot()
    { 
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _player.ShootMask))
        {
            Vector3 shootDirection = (hit.point - _player.FirePoint.position).normalized;
            shootDirection.y = 0;
            onShoot?.Invoke(shootDirection);
        }
    }

    public void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
            onReload?.Invoke();
    }

    public void HandleUlitimate()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            onUseUltimate?.Invoke();
    }
}

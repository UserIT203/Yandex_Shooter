using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName = "Player Ultimate/Laser")]
public class LaserUltimate : UltimateBase
{
    [Header("Laser Settings")]
    [SerializeField] private float _damage;
    [SerializeField] private Laser _laserPrefab;
    [SerializeField] private LayerMask _mask;

    private Laser _laserObject;

    protected override void Execute()
    {
        base.Execute();
        _laserObject = Instantiate(_laserPrefab, _player.transform.position, Quaternion.identity);
        _laserObject.transform.SetParent(_player.transform, false);
        _laserObject.SetTarget(_player, _damage, _mask);
    }

    protected override void CleanUp()
    {
        Destroy(_laserObject.gameObject);
    }
}

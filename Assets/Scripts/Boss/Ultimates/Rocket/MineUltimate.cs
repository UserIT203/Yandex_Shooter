using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineUltimate : UltimateBase
{
    [Header("Mine Ultimate Settings")]
    [SerializeField] private float _mineDamage;
    [SerializeField] private Mine _minePrefab;

    [Header("Mine Spawn Options")]
    [SerializeField] private float _mineSpawnArea;
    [SerializeField] private float _createMineDealy;
    [SerializeField] private int _maxMineCount;
    [SerializeField] private float _minDistance;

    protected override void Execute()
    {
        base.Execute();
    }

    protected override void CleanUp()
    {
        base.CleanUp();
    }

    private IEnumerator CreateMine()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_createMineDealy);

        while (_maxMineCount > 0)
        {
            Mine mine = Instantiate(_minePrefab);

            yield return waitingTime;
        }
    }
}

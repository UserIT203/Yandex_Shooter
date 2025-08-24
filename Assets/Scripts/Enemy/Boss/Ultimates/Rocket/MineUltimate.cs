using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

[CreateAssetMenu(fileName = "Mine", menuName = "Boss Ultimate/Mine")]
public class MineUltimate : UltimateBase
{
    [Header("Mine Ultimate Settings")]
    [SerializeField] private Mine _minePrefab;

    [Header("Mine Spawn Settings")]
    [SerializeField] private float _mineSpawnArea;
    [SerializeField] private float _createMineDealy;
    [SerializeField] private int _maxMineCount;
    [SerializeField] private float _minDistance;

    [Header("Mine Settings")]
    [SerializeField] private float _mineDamage;
    [SerializeField] private float _mineActivateRadius;

    private List<Vector3> _spawnPoints;

    public override void Initialized(Player player)
    {
        base.Initialized(player);
    }

    protected override void Execute()
    {
        base.Execute();
        _player.StartCoroutine(CreateMine());
    }

    protected override void CleanUp()
    {
        base.CleanUp();
    }

    private IEnumerator CreateMine()
    {
        GetSpawnPoints();

        int currentIndex = 0;

        WaitForSeconds waitingTime = new WaitForSeconds(_createMineDealy);

        while (currentIndex < _maxMineCount)
        {
            Mine mine = Instantiate(_minePrefab, _boss.transform.position, Quaternion.identity);
            mine.SetTarget(_spawnPoints[currentIndex], _mineDamage, _mineActivateRadius);

            currentIndex++;

            yield return waitingTime;
        }
    }

    private void GetSpawnPoints()
    {
        _spawnPoints.Clear();

        for (int i = 0; i < _maxMineCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();

            bool validPosition = true;
            foreach (var pos in _spawnPoints)
            {
                if (Vector3.Distance(spawnPosition, pos) < _minDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            if (validPosition == true)
                _spawnPoints.Add(spawnPosition);
            else
                i--;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * _mineSpawnArea;
        Vector3 position = _player.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        
        return position;
    }
}

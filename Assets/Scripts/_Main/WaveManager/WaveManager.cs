using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaveManager : MonoBehaviour, IEnemyObserver
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Spawner _spawner;

    private int _currentWave;
    private int _maxWave;
    private int _currentEnemiesInWave;

    private void Awake()
    {
        _maxWave = _waves.Count;
        InitilizedWave();
    }

    public void OnEnemyDestroed()
    {
        Debug.Log("Enemy Die");
        _currentEnemiesInWave--;
        CheackWaveState();
    }

    private void CheackWaveState()
    {
        if (_currentEnemiesInWave > 0) return;

        Debug.Log("Волна кончилась");

        if (_currentEnemiesInWave <= 0)
            EndWave();
    }

    private void EndWave()
    {
        if (_currentWave == _maxWave - 1)
        {
            return;
        }

        _currentWave++;
        InitilizedWave();
    }

    private void InitilizedWave() 
    { 
        _currentEnemiesInWave = _waves[_currentWave].EnemyCount;
        _spawner.StartSpawning(_waves[_currentWave].Enemies, _currentEnemiesInWave,
            this);
    }
}

[System.Serializable]
public class Wave
{
    public List<EnemyInSpawner> Enemies;
    public int EnemyCount { get => GetEnemiesCount(); }

    private int GetEnemiesCount()
    {
        int count = 0;

        foreach (var enemy in Enemies) 
        {
            count += enemy.EnemyCount;
        }

        return count;
    }
}

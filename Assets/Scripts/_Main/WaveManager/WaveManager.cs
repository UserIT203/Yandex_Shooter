using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour, IEnemyObserver
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Spawner _spawner;

    private int _currentWave;
    private int _maxWave;
    private int _currentEnemiesInWave;
    private bool _bossDie;
    private GameManager _gameManager;

    public event Action<int> onStartWave;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
        _gameManager.onGameStart += InitilizedWave;
    }

    private void OnDisable()
    {
        _gameManager.onGameStart -= InitilizedWave;
    }

    private void Start()
    {
        if (SceneTransition.Instance == null)
            InitilizedWave();

        _maxWave = _waves.Count;
    }

    public void OnEnemyDestroed()
    {
        _currentEnemiesInWave--;

        if (_currentEnemiesInWave <= 0 && _waves[_currentWave].HasBoss == true)
            _spawner.CreateBoss();

        CheackWaveState();
    }

    public void OnBossDestroed()
    {
        _bossDie = true;
        CheackWaveState();
    }

    private void CheackWaveState()
    {
        if (_currentEnemiesInWave > 0 || _bossDie == false) return;

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
        if(_waves[_currentWave].HasBoss)
            _bossDie = false;

        _currentEnemiesInWave = _waves[_currentWave].EnemyCount;
        _spawner.StartSpawning(_waves[_currentWave].Enemies, _currentEnemiesInWave,
            this, _currentWave);

        onStartWave?.Invoke(_currentWave);
    }
}

[System.Serializable]
public class Wave
{
    public List<EnemyInSpawner> Enemies;
    public int EnemyCount { get => GetEnemiesCount(); }
    public bool HasBoss;

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

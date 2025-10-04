using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject] private GameTimeManager _timeManager;
    [Inject] private ItemUseContext _itemUseContext;
    [Inject] private BossUI _bossUI;

    [Header("Main Settings")]
    [SerializeField] private List<Enemy> _enemiesPrefab;
    [SerializeField] private List<Boss> _bosesPrefab;
    [SerializeField] private EnemyConfig _enemyIncreaseConfig;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private Player _player;
    [SerializeField] private bool _canSpawn;

    [Header("Spawning Zone")]
    [SerializeField] private Vector2 _spawnAreaSize;

    [Header("Pool Settings")]
    [SerializeField] private Bullet _enemyBullet;
    [SerializeField] private int _bulletCount;
    [SerializeField] private Transform _bulletPoolContainer;

    private int _currentWave;
    private int _enemyCount;
    private List<EnemyInSpawner> _enemies;
    private int _currentEnemyCount;
    private Coroutine _spawnCoroutine;
    private IEnemyObserver _enemiesObserver;
    private CustomPool<Bullet> _enemyBulletPool;

    private void OnEnable()
    {
        _timeManager.OnGamePaused += Freeze;
        _timeManager.OnGameResumed += Unfreeze;
    }

    private void OnDisable()
    {
        _timeManager.OnGamePaused -= Freeze;
        _timeManager.OnGameResumed -= Unfreeze;
    }

    private void Awake()
    {
        _enemyBulletPool = new CustomPool<Bullet>(
           _enemyBullet,
           _bulletCount,
           _bulletPoolContainer);
    }

    public void StartSpawning(List<EnemyInSpawner> enemies, int enemiesCount,
        IEnemyObserver enemyObserver, int currentWave)
    {
        _canSpawn = true;
        _currentEnemyCount = 0;
        _enemyCount = enemiesCount;
        _currentWave = currentWave;

        _enemiesObserver = enemyObserver;
        _enemies = enemies;
        _enemyCount = enemiesCount;

        StartSpawningEnemy();   
    }

    private void StartSpawningEnemy()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_spawnInterval);

        while(_canSpawn == true)
        {
            if (_currentEnemyCount < _enemyCount)
                CreateEnemy();
            else
                _canSpawn = false;

            yield return waitingTime;
        }

        _spawnCoroutine = null;
    }

    private void CreateEnemy()
    {
        int enemyTypeIndex = Random.Range(0, _enemies.Count);
        EnemyInSpawner enemyType = _enemies[enemyTypeIndex];
        
        if (enemyType.EnemyCount <= 0) return;

        enemyType.SpawnEnemy();

        Vector3 spawnPosition = GetSpawnPosition();

        Enemy newEnemy = Instantiate(_enemiesPrefab[(int)enemyType.EnemyType],
            spawnPosition, Quaternion.identity);

        newEnemy.Initialized(_player, _enemiesObserver, _enemyBulletPool, _itemUseContext, _timeManager);

        for (int i = 0; i < _currentWave; i++)
        {
            newEnemy.Stats.ApplyModifiers(_enemyIncreaseConfig);
        }

        _currentEnemyCount++;
    }

    private Vector3 GetSpawnPosition()
    {
        float x = transform.position.x + Random.Range(-_spawnAreaSize.x / 2, 
            _spawnAreaSize.x / 2);
        float z = transform.position.z + Random.Range(-_spawnAreaSize.y / 2, 
            _spawnAreaSize.y / 2);

        return new Vector3(x, transform.position.y, z);
    }

    private void Freeze()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private void Unfreeze()
    {
        if (_spawnCoroutine == null) _spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    public void CreateBoss()
    {
        int randomValue = Random.Range(0, _bosesPrefab.Count);
        Vector3 spawnPosition = GetSpawnPosition();

        Boss newBoss = Instantiate(_bosesPrefab[randomValue], spawnPosition, Quaternion.identity);
        newBoss.Initialized(_player, _enemiesObserver, _enemyBulletPool, _itemUseContext, _timeManager, this);

        _bossUI.Initialized(newBoss);
    }

    public void CreateEnemiesFromBoss(List<EnemyInSpawner> enemies)
    {
        _enemies = enemies;
        _currentEnemyCount = 0;
        _enemyCount = enemies[0].EnemyCount;
        _canSpawn = true;
        _enemiesObserver = null;
        StartSpawningEnemy();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_spawnAreaSize.x, 
            0.1f, _spawnAreaSize.y));
    }
}

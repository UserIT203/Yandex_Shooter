using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private float _updatePathTimer;

    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Transform _target;
    private float _timer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();

        _enemy.onInitialized += Initialized;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _updatePathTimer)
        {
            _agent.SetDestination(_target.position);
            _timer = 0;
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Initialized(Player target, CustomPool<Bullet> bulletPool = null)
    {
        _agent.stoppingDistance = _enemy.Stats.RadiusAttack.GetValue();
        _agent.speed = _enemy.Stats.Speed.GetValue();

        _target = target.transform;
    }
}
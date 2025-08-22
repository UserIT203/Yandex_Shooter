using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private const float DetactionRadius = 0.5f;

    [Header("Orbit Settings")]
    [SerializeField] private float _orbitRadius = 3f; // Радиус орбиты
    [SerializeField] private float _orbitHeight = 1.5f; // Высота над игроком
    [SerializeField] private float _orbitSpeed = 180f; // Скорость вращения (градусов/сек)
    [SerializeField] private float _smoothFollowSpeed = 10f; // Скорость следования

    [Header("Damage Settings")]
    [SerializeField] private float _damageInterval;

    private Transform _player;

    private float _currentAngle = 0f; // Текущий угол в градусах
    private Vector3 _lastPlayerPosition;
    private Vector3 _playerVelocity;

    private List<Collider> _enemiesCollder = new List<Collider>();
    private LayerMask _enemiesMask;
    private float _lastAttackTimer = 0f;
    private float _damage;

    private void Update()
    {
        if (_player == null) return;

        Vector3 currentPlayerPosition = _player.position;
        _playerVelocity = (currentPlayerPosition - _lastPlayerPosition) / Time.deltaTime;
        _lastPlayerPosition = currentPlayerPosition;

        if (_playerVelocity.magnitude > 0.1f)
        {
            UpdateOrbitAngle();
        }

        CheckEnemiesInRange();
        ApplyDamageToEnemies();
        UpdateOrbPosition();
    }

    private void SetInitialPosition()
    {
        Vector3 playerForward = _player.forward;
        _currentAngle = Mathf.Atan2(playerForward.x, playerForward.z) * Mathf.Rad2Deg + 180f;
        UpdateOrbPosition();
    }

    private void UpdateOrbitAngle()
    {
        // Преобразуем горизонтальное движение в изменение угла
        Vector3 horizontalVelocity = new Vector3(_playerVelocity.x, 0f, _playerVelocity.z);

        if (horizontalVelocity.magnitude > 0.1f)
        {
            // Определяем направление движения
            Vector3 movementDirection = horizontalVelocity.normalized;

            // Вычисляем угол направления движения
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;

            // Плавно поворачиваем к направлению движения
            float angleDifference = Mathf.DeltaAngle(_currentAngle, targetAngle);

            // Применяем вращение с учетом скорости
            float rotationAmount = _orbitSpeed * Time.deltaTime;
            _currentAngle += Mathf.Clamp(angleDifference, -rotationAmount, rotationAmount);
        }
    }

    private void UpdateOrbPosition()
    {
        // Вычисляем позицию по окружности
        float angleRad = _currentAngle * Mathf.Deg2Rad;

        // Позиция относительно игрока
        Vector3 orbitPosition = new Vector3(
            Mathf.Sin(angleRad) * _orbitRadius,
            _orbitHeight,
            Mathf.Cos(angleRad) * _orbitRadius
        );

        // Целевая мировая позиция
        Vector3 targetWorldPosition = _player.position + orbitPosition;

        transform.position = Vector3.Lerp(transform.position, targetWorldPosition,
                                        _smoothFollowSpeed * Time.deltaTime);

        transform.LookAt(_player);
    }

    private void CheckEnemiesInRange()
    {
        _enemiesCollder.Clear();

        _enemiesCollder = Physics.OverlapSphere(transform.position,
            DetactionRadius, _enemiesMask).ToList();

        Debug.Log(_enemiesCollder.Count);
    }

    private void ApplyDamageToEnemies()
    {
        if(Time.time - _lastAttackTimer >= _damageInterval)
        {
            foreach (Collider enemy in _enemiesCollder)
            {
                Debug.Log("Enemy Take Damage");
                enemy.GetComponent<IDamagable>().TakeDamage(_damage);
            }

            _lastAttackTimer = Time.time;
        }
    }

    public void SetTarget(Player player, float damage, LayerMask mask)
    {
        _player = player.transform;
        _enemiesMask = mask;
        _damage = damage;
        _lastPlayerPosition = _player.position;
        SetInitialPosition();
    }
}

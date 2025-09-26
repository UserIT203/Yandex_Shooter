using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Основные параметры")]
    [SerializeField] private float _speed = 10f;                   // Скорость движения
    [SerializeField] private float _trackingStrength = 3f;         // Сила наведения
    [SerializeField] private float _homingDelay = 1f;              // Задержка наведения
    [SerializeField] private bool _predictTargetPosition = true;   // Предсказывать позицию цели
    [SerializeField] private float _lifeTime;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _explosionEffect;

    private float _damage;
    private Transform _target;
    private Vector3 _startPosition;
    private Vector3 _initialTargetPosition;
    private float _journeyLength;
    private float _startTime;
    private Vector3 _velocity;
    private bool _canHome = false;
    private Vector3 _lastPosition;
    private Coroutine _coroutine;

    private void Start()
    {
        _lastPosition = transform.position;

        // Запускаем задержку наведения
        if (_homingDelay > 0)
        {
            Invoke(nameof(EnableHoming), _homingDelay);
        }
        else
        {
            _canHome = true;
        }
    }

    private void InitializeProjectile()
    {
        if (_target == null)
        {
            Debug.LogWarning("Цель не назначена для снаряда!");
            Destroy(gameObject);
            return;
        }

        _startPosition = transform.position;
        _initialTargetPosition = _target.position;

        Vector3 initialDirection = (_initialTargetPosition - _startPosition).normalized;
        _velocity = initialDirection * _speed;
        _journeyLength = Vector3.Distance(_startPosition, _initialTargetPosition);
        _startTime = Time.time;
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (_target == null)
        {
            // Если цель уничтожена, продолжаем движение по инерции
            transform.position += _velocity * Time.deltaTime;
            RotateTowardsMovement();
            return;
        }

        transform.LookAt(_target);

        // Получаем текущую позицию цели (с предсказанием)
        Vector3 currentTargetPosition = _target.position;
        if (_predictTargetPosition)
        {
            currentTargetPosition = PredictTargetPosition();
        }

        // Вычисляем направление к цели
        Vector3 directionToTarget = (currentTargetPosition - transform.position).normalized;

        // Применяем кривую к траектории
        Vector3 curvedDirection = ApplyTrajectoryCurve(directionToTarget);

        // Плавное наведение на цель
        if (_canHome)
        {
            _velocity = Vector3.Lerp(_velocity, curvedDirection * _speed, _trackingStrength * Time.deltaTime);
        }
        else
        {
            // В начальный период просто добавляем кривую к базовому направлению
            _velocity = curvedDirection * _speed;
        }

        // Обновляем позицию
        transform.position += _velocity * Time.deltaTime;
        RotateTowardsMovement();

        // Проверяем близость к цели
        if (Vector3.Distance(transform.position, currentTargetPosition) < 0.5f)
        {
            OnTargetReached();
        }
    }

    private Vector3 ApplyTrajectoryCurve(Vector3 directionToTarget)
    {
        Vector3 resultDirection = directionToTarget;

        // Добавляем параболическое отклонение на основе времени
        float timeSinceStart = Time.time - _startTime;
        float parabolaOffset = Mathf.Sin(timeSinceStart * 2f) * 0.3f;
        resultDirection += Vector3.up * parabolaOffset;

        // Добавляем боковое колебание для более изогнутой траектории
        Vector3 right = Vector3.Cross(directionToTarget, Vector3.up).normalized;
        float sideOffset = Mathf.Sin(timeSinceStart * 3f) * 0.2f;
        resultDirection += right * sideOffset;

        return resultDirection.normalized;
    }

    private Vector3 PredictTargetPosition()
    {
        if (_target == null) return _target.position;

        // Простое предсказание позиции цели на основе её скорости
        Rigidbody targetRb = _target.GetComponent<Rigidbody>();
        Vector3 targetVelocity = Vector3.zero;

        targetVelocity = (_target.position - _target.position) / Time.deltaTime;

        // Предсказываем позицию через время, необходимое для достижения цели
        float timeToTarget = Vector3.Distance(transform.position, _target.position) / _speed;
        return _target.position + targetVelocity * timeToTarget * 0.5f; // Коэффициент 0.5 для более консервативного предсказания
    }

    private void RotateTowardsMovement()
    {
        if (_velocity != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(_velocity.normalized);
        }
    }

    private void EnableHoming()
    {
        _canHome = true;
    }

    private void OnTargetReached()
    {
        Debug.Log("Снаряд достиг цели!");
        if (_target.TryGetComponent<IDamagable>(out var unit))
            unit.TakeDamage(_damage);

        StopCoroutine(_coroutine);
        Destroy(gameObject);
    }

    private IEnumerator DestroyToTimeLife()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }

    public void SetTarget(Transform newTarget, float damage)
    {
        _damage = damage;
        _target = newTarget;

        _coroutine = StartCoroutine(DestroyToTimeLife());

        InitializeProjectile();
    }

    private void OnDestroy()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
    }
}

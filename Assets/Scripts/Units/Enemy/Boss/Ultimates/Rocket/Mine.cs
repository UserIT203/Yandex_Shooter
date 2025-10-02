using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _height = 5f;      // Максимальная высота траектории
    [SerializeField] private float _duration = 2f;    // Время полёта
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private ParticleSystem _circel;
    [SerializeField] private ParticleSystem _explosionParticle;

    private float _damage;
    private float _triggerRadius;
    private bool _isActive = false;

    private ParticleSystem _mineParticle;
    private Vector3 _targetPosition;
    private Vector3 _startPoint;
    private float _elapsedTime = 0f;

    private void Awake()
    {
        _mineParticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        CheackTriggerZone();
    }

    public void SetTarget(Vector3 target, float damage, float radius)
    {
        _startPoint = transform.position;
        _targetPosition = target;

        _damage = damage;
        _triggerRadius = radius;
        _mineParticle.Play();

        StartCoroutine(LifeTime());
        StartCoroutine(MoveProjectile());
    }

    private IEnumerator MoveProjectile()
    {
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / _duration;
            _elapsedTime += Time.deltaTime;

            Vector3 newPos = Vector3.Lerp(_startPoint, _targetPosition, progress);

            float arc = Mathf.Sin(progress * Mathf.PI) * _height;
            newPos.y += arc;

            transform.position = newPos;

            yield return null;
        }

        transform.position = _targetPosition;

        yield return new WaitForSeconds(1f);

        _isActive = true;

        _circel.Play();
        Debug.Log("Снаряд достиг цели!");
    }

    private void CheackTriggerZone()
    {
        if(_isActive == false) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _triggerRadius);

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent<Player>(out var target))
            {
                Explosion(target);
            }
        }
    }

    private void Explosion(IDamagable target)
    {
        target.TakeDamage(_damage);
        Instantiate(_explosionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _triggerRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private const string EnemyTag = "Enemy";

    [Header("Настройки орбиты")]
    [SerializeField] private Transform center;
    [SerializeField] private float radius = 5f;

    private float _damage;
    private float _degreesPerSecond = 45f;

    private float _initialY;

    private void Start()
    {
        _initialY = transform.position.y;
    }

    private void Update()
    {
        Vector3 centerPoint = center != null
            ? new Vector3(center.position.x, _initialY, center.position.z)
            : new Vector3(0, _initialY, 0);

        transform.RotateAround(centerPoint, Vector3.up, _degreesPerSecond * Time.deltaTime);

        Vector3 desiredPos = (transform.position - centerPoint).normalized * radius + centerPoint;
        desiredPos.y = _initialY;
        transform.position = desiredPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == EnemyTag)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
                enemy.Stats.TakeDamage(_damage);
                
            Debug.Log("Sword: Enemy " + _damage);
        }
    }

    public void UnlockSword() => gameObject.SetActive(true);

    public void UpdateOptions(float damage, float speed)
    {
        _damage = damage;
        _degreesPerSecond = speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _height = 5f;      // Максимальная высота траектории
    [SerializeField] private float _duration = 2f;    // Время полёта

    private Vector3 _targetPosition;
    private Vector3 _startPoint;
    private float _elapsedTime = 0f;

    public void SetTarget(Vector3 target)
    {
        _startPoint = transform.position;
        _targetPosition = target;
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

        Debug.Log("Снаряд достиг цели!");
    }
}

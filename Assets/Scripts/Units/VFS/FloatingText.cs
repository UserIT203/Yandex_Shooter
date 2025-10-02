using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class FloatingText : MonoBehaviour
{
    [Header("Floating Text Settings")]
    [SerializeField] private float _timeToLife;
    [SerializeField] private Vector3 _offset;

    [Header("Move Animation Settings")]
    [SerializeField] private float _moveDistance;
    [SerializeField] private Ease _moveEase = Ease.OutQuad;

    private TextMesh _textMesh;
    private Sequence _animationSequence;

    private void OnDisable()
    {
        _animationSequence?.Kill();
    }

    private void Awake()
    {
        _textMesh = GetComponent<TextMesh>();
    }

    public void SetSettings(float text, Color color)
    {
        _textMesh.text = text.ToString();
        _textMesh.color = color;
        transform.position += _offset;

        PlayAnimation();
    }

    private Vector3 GetRandomDirection()
    {
        Vector3[] directions = { Vector2.up, Vector2.left, Vector2.right };
        return directions[Random.Range(0, directions.Length)];
    }

    private void PlayAnimation()
    {
        _animationSequence = DOTween.Sequence();
        Vector2 targetPosition = transform.position + (GetRandomDirection() * _moveDistance);


        _animationSequence.Insert(0, transform.DOMove(targetPosition, _timeToLife))
            .SetEase(_moveEase);

        _animationSequence.OnComplete(() =>Destroy(gameObject));

        _animationSequence.Play();
    }

    private void OnDestroy()
    {
        _animationSequence?.Kill();
    }
}

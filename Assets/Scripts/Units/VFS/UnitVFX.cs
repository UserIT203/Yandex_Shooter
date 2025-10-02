using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UnitVFX : MonoBehaviour
{
    [Header("Flash Effect Settings")]
    [SerializeField] private Material _hitMaterialTemplate;
    [SerializeField] private float _flashDuration = 0.2f;
    [SerializeField] private Color _flashColor;
    [SerializeField] private AnimationCurve _intensityCurve = null;

    [Header("Floating Text")]
    [SerializeField] private Color _colorFloatingText;
    [SerializeField] private bool _canPlayFloatingText = true;
    [SerializeField] private FloatingText _floatingTextPrefab;

    [Header("Blood Effect Settings")]
    [SerializeField] private ParticleSystem _bloodEffect;
    
    private SpriteRenderer _spriteRenderer;
    private Material _originMaterial, _hitMaterial;
    private Coroutine _flashCoroutine;

    private void OnEnable()
    {
        transform.root.GetComponent<IDamagable>().onTakeDamage += HitVFX;
    }

    private void OnDisable()
    {
        transform.root.GetComponent<IDamagable>().onTakeDamage -= HitVFX;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originMaterial = _spriteRenderer.material;
        
        _hitMaterial = new Material(_hitMaterialTemplate);
        _hitMaterial.color = _flashColor;

        if (_intensityCurve == null)
        {
            _intensityCurve = new AnimationCurve(
                new Keyframe(0f, 1f, 0f, 1f),
                new Keyframe(1f, 0f, 0f, 0f)
            );
            _intensityCurve.postWrapMode = WrapMode.Once;
        }
    }

    private void HitVFX(float damage)
    {
        if(_flashCoroutine == null) _flashCoroutine = StartCoroutine(PlayFlashVFX());
        if (_canPlayFloatingText) CreateFloatingText(damage);
        
        PlayBloodEffect();
    }

    private void CreateFloatingText(float damage)
    {
        FloatingText floatingText = Instantiate(_floatingTextPrefab, transform.position, Quaternion.identity);
        floatingText.transform.SetParent(transform.root, true);

        floatingText.SetSettings(damage, _colorFloatingText);
    }

    private IEnumerator PlayFlashVFX()
    {
        Color originalColor = _spriteRenderer.color;
        float elapsedTime = 0f;

        _spriteRenderer.material = _hitMaterial;

        while (elapsedTime < _flashDuration)
        {
            float t = elapsedTime / _flashDuration;
            float intensity = _intensityCurve.Evaluate(t);

            _spriteRenderer.color = Color.Lerp(originalColor * intensity, originalColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _spriteRenderer.color = Color.white;
        _spriteRenderer.material = _originMaterial;
        _flashCoroutine = null;
    }

    private void PlayBloodEffect()
    {
        if (_bloodEffect == null) return;

        _bloodEffect.Play();
    }

    protected virtual void DestroyAction()
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = null;
    }

    private void OnDestroy()
    {
        DestroyAction();
    }
}

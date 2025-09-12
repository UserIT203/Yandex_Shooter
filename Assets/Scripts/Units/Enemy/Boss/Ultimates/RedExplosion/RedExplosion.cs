using UnityEngine;

public class RedExplosion : MonoBehaviour
{
    [SerializeField] private HitHandler _hitBox;

    private float _radius;
    private float _damage;
    private Player _target;


    private void OnEnable()
    {
        _hitBox.onCollision += DoDamagePlayer;
    }

    private void OnDisable()
    {
        _hitBox.onCollision -= DoDamagePlayer;
    }

    private void DoDamagePlayer()
    { 

        float distance = Vector3.Distance(transform.position, _target.transform.position);

        if (distance <= _radius)
            _target.TakeDamage(_damage);

        Destroy(gameObject);
    }

    public void Initialized(float radius, float damage, Player player)
    {
        _radius = radius;
        _damage = damage;
        _target = player;
    }
}

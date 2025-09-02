using System;

public interface IDamagable
{
    public event Action<float> onTakeDamage;
    public void TakeDamage(float damage);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public event Action onCollision;

    private void OnParticleCollision(GameObject other)
    {
        onCollision?.Invoke();
    }
}

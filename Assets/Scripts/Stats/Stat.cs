using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] private float _baseValue;
    [SerializeField] private List<float> _modificators;

    public float GetValue()
    {
        float value = _baseValue;

        foreach (float modificator in _modificators)
        {
            value += modificator;
        }

        return value;
    }

    public void Reset()
    {
        _modificators.Clear();
    }

    public void AddModifier(float modificator)
    {
        _modificators.Add(modificator);
    }
}

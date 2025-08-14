using System.Collections.Generic;
using UnityEngine;

public class CustomPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Queue<T> _objects;
    private Transform _poolContainer;

    public CustomPool(T prefab, int prewareObjects, Transform poolContainer)
    {
        _prefab = prefab;
        _objects = new Queue<T>();
        _poolContainer = poolContainer;

        for (int i = 0; i < prewareObjects; i++)
        {
            var obj = GameObject.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolContainer, false);
            _objects.Enqueue(obj);
        }
    }

    public T Get()
    {
        var obj = _objects.Dequeue();

        if (obj == null)
            obj = Create();

        obj.gameObject.SetActive(true);

        return obj;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.position = Vector3.zero;

        _objects.Enqueue(obj);
    }

    private T Create()
    {
        var obj = GameObject.Instantiate(_prefab);
        _objects.Enqueue(obj);
        return _objects.Dequeue();
    }
}

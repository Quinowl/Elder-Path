using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{

    private readonly T prefab;
    private readonly Transform parentTransform;
    private readonly Queue<T> pool = new Queue<T>();

    public ObjectPool(T prefab, int initialSize = 10, Transform parentTransform = null)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;
        for (int i = 0; i < initialSize; i++) AddObjectToPool();
    }

    public T Get()
    {
        if (pool.Count == 0) AddObjectToPool();
        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    private void AddObjectToPool()
    {
        T obj = Object.Instantiate(prefab, parentTransform);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
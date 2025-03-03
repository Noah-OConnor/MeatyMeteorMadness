using System.Collections.Generic;
using UnityEngine;

// DESIGN PATTERN - Object Pool
// Implements the Object Pool pattern by maintaining a pool of reusable objects.
// Instead of frequently creating and destroying objects, inactive objects are stored and reactivated when needed,
// improving performance and memory efficiency.

public class ObjectPool<T> where T : FallingObject
{
    private List<T> pool = new List<T>();
    private FallingObjectFactory<T> factory;
    private int initialSize;

    public ObjectPool(FallingObjectFactory<T> factory, int initialSize)
    {
        this.initialSize = initialSize;
        this.factory = factory;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = factory.Create();
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public T GetObject()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        T newObj = factory.Create();
        pool.Add(newObj);
        return newObj;
    }

    public void RemoveExtraObjects()
    {
        while (pool.Count > initialSize)
        {
            GameObject.Destroy(pool[pool.Count - 1].gameObject);
            pool.RemoveAt(pool.Count - 1);
        }

        foreach (FallingObject fallingObject in pool)
        {
            fallingObject.DisableSelf();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> pool = new List<T>();
    private T prototype;

    public ObjectPool(T prototype, int initialSize)
    {
        this.prototype = prototype;
        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prototype);
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

        T newObj = GameObject.Instantiate(prototype);
        pool.Add(newObj);
        return newObj;
    }
}

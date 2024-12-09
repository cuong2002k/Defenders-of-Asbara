using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefabs;
    private int _maxSize;
    private Transform _parent;
    private Queue<T> _availableObject = new Queue<T>();

    public ObjectPool([NotNull] T prefabs, Transform parent = null, int initSize = 1, int maxSize = 10)
    {
        this._prefabs = prefabs;
        this._parent = parent ?? new GameObject($"{typeof(GameObject).Name}Pool").transform;
        this._maxSize = maxSize;

        for (int i = 0; i < initSize; i++)
        {
            CreatePool();
        }
    }

    private T CreatePool()
    {
        T newObj = GameObject.Instantiate(_prefabs, _parent) as T;
        newObj.gameObject.SetActive(false);
        this._availableObject.Enqueue(newObj);
        return newObj;

    }

    public T GetPool()
    {
        if (this._availableObject.Count == 0)
        {
            this.CreatePool();
        }
        T obj = this._availableObject.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(T obj)
    {
        if (obj == null) return;
        obj.gameObject.SetActive(false);
        this._availableObject.Enqueue(obj);
    }

    public void ClearPool()
    {
        this._availableObject.Clear();
    }

}

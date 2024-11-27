using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private List<PoolAble> _poolList = new List<PoolAble>();
    private Dictionary<GameObject, List<GameObject>> _poolContainer;

    private void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        _poolContainer = new Dictionary<GameObject, List<GameObject>>();
        foreach (PoolAble poolAble in _poolList)
        {

            for (int i = 0; i < poolAble.Capacity; i++)
            {
                GameObject objectPool = this.CreateInstance(poolAble.gameObject, this.transform);
                objectPool.SetActive(false);
                this.AddObjectPool(poolAble.gameObject, objectPool);
            }
        }
    }

    /// <summary>
    /// Get object form pool
    /// </summary>
    /// <param name="prefab">What object need get</param>
    /// <returns></returns>
    public GameObject GetObjectPool(GameObject prefab)
    {
        Common.Warning(prefab != null, "Object get pool is null {0}", prefab);
        if (_poolContainer.ContainsKey(prefab))
        {
            foreach (GameObject objectPool in this._poolContainer[prefab])
            {
                if (objectPool.activeSelf == false)
                {
                    objectPool.SetActive(true);
                    return objectPool;
                }
            }
        }
        GameObject objectInstance = this.CreateInstance(prefab, this.transform);
        AddObjectPool(prefab, objectInstance);
        return objectInstance;

    }

    /// <summary>
    /// Add object back to pool for reuse
    /// </summary>
    /// <param name="objectToAdd"></param>
    public void AddObjectPool(GameObject key, GameObject objectToAdd)
    {
        if (this._poolContainer.ContainsKey(key))
        {
            this._poolContainer[key].Add(objectToAdd);
        }
        else
        {
            this._poolContainer.Add(key, new List<GameObject>());
            this._poolContainer[key].Add(objectToAdd);
        }
    }

    /// <summary>
    /// Create instance object
    /// </summary>
    /// <param name="gameObject">Object need instance</param>
    /// <returns></returns>
    private GameObject CreateInstance(GameObject gameObject, Transform parent)
    {
        // instance object and set parent for object      
        return Instantiate(gameObject, parent);
    }

}

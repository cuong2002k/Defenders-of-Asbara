using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public PoolAble[] poolAbles;
    private Dictionary<PoolAble, ObjectPool<PoolAble>> _poolContainer = new Dictionary<PoolAble, ObjectPool<PoolAble>>();

    private void Start()
    {
        for (int i = 0; i < poolAbles.Length; i++)
        {
            this.CreatePool(poolAbles[i], this.transform);
        }
    }

    public ObjectPool<PoolAble> CreatePool(PoolAble poolAble, Transform parent)
    {
        if (this._poolContainer.ContainsKey(poolAble))
        {
            Common.LogWarning($"Pool for type {poolAble.gameObject.name} already exit", poolAble);
            return this._poolContainer[poolAble];
        }
        ObjectPool<PoolAble> newPool = new ObjectPool<PoolAble>(poolAble, parent, poolAble.InitSize, poolAble.MaxSize);
        this._poolContainer[poolAble] = newPool;
        return newPool;
    }

    private ObjectPool<PoolAble> GetPool(PoolAble prefabs)
    {
        if (_poolContainer.TryGetValue(prefabs, out ObjectPool<PoolAble> pool))
        {
            return pool;
        }
        return this.CreatePool(prefabs, this.transform);
    }

    public GameObject GetPoolItem(PoolAble prefabs)
    {
        ObjectPool<PoolAble> objectPool = this.GetPool(prefabs);
        PoolAble poolAble = objectPool.GetPool();
        poolAble.PoolAvailable = objectPool;
        return poolAble.gameObject;
    }

    public void Return(PoolAble obj)
    {
        PoolAble poolAble = obj.GetComponent<PoolAble>();
        if (poolAble != null)
        {
            this.GetPool(obj).Return(poolAble);
        }
    }

    // public void ClearPool(GameObject obj)
    // {
    //     this.GetPool(obj).ClearPool();
    // }

    // public void ClearAllPool()
    // {
    //     this._poolContainer.Clear();
    // }
}

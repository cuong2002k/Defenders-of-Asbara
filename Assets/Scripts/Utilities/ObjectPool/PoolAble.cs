using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAble : MonoBehaviour
{
    private ObjectPool<PoolAble> _poolAvailable;

    public ObjectPool<PoolAble> PoolAvailable
    {
        set
        {
            this._poolAvailable = value;
        }
    }

    public void TryPool()
    {
        this.transform.SetParent(PoolManager.Instance.transform, true);
        this._poolAvailable.Return(this);
    }

    public static GameObject TryGetPool(GameObject prefabs)
    {
        var poolAble = prefabs.GetComponent<PoolAble>();
        return poolAble != null && PoolManager.Instance.HasInstance() ?
         PoolManager.Instance.GetPoolItem(poolAble) : Instantiate(prefabs);
    }

    public static void TryReturn(GameObject gameObject)
    {
        PoolAble poolAble = gameObject.GetComponent<PoolAble>();
        if (poolAble != null && PoolManager.Instance.HasInstance())
        {
            poolAble.TryPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static T TryGetPool<T>(GameObject prefabs) where T : Component
    {
        var poolAble = prefabs.GetComponent<PoolAble>();

        return poolAble != null && PoolManager.Instance.HasInstance() ?
        PoolManager.Instance.GetPoolItem(poolAble).GetComponent<T>() :
        Instantiate(prefabs).GetComponent<T>();
    }

    private void OnSpawnPool(PoolAble poolAble)
    {
        var pool = poolAble.GetComponent<IPoolAble>();
        if (pool != null)
        {
            pool.OnSpawn();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAble : MonoBehaviour, IPoolAble
{
    private ObjectPool<PoolAble> _poolAvailable;
    [SerializeField] private int _maxSize = 1;
    [SerializeField] private int _initSize = 1;

    public int MaxSize => _maxSize;
    public int InitSize => _initSize;

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

    public virtual void OnSpawn()
    { }

    public virtual void OnDespawn()
    {
        PoolAble.TryReturn(this.gameObject);
    }
}

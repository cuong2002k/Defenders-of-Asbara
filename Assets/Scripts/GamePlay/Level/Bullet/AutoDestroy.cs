using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : TimerBehavior
{
    private RepeatTimer _repeatTimer;
    [SerializeField] private float _destroyTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _repeatTimer = new RepeatTimer(_destroyTimer, DestroyObject);
        this.StartTimer(_repeatTimer);
    }

    private void DestroyObject()
    {
        PoolAble.TryReturn(this.gameObject);
    }



}

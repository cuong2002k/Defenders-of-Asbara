using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    /// <summary>
    /// Event is trigger when time elapses
    /// </summary>
    private readonly Action _callBack;

    /// <summary>
    /// time using 
    /// </summary>
    [SerializeField]private float _currentTime, _timer;

    /// <summary>
    ///  Timer contructer
    /// </summary>
    /// <param name="newTimer"> The time that timer is counting </param>
    /// <param name="callBack"> event call when time elapses</param>
    public Timer(float newTimer, Action callBack)
    {
        this.SetTimer(newTimer);

        this._currentTime = 0;
        this._callBack += callBack;
    }

    /// <summary>
    /// return Asset time
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns>true if time elapses, false if otherwise </returns>

    public virtual bool Tick(float deltaTime)
    {
        return AssetTime(deltaTime);
    }


    /// <summary>
    /// check if time elapse active event
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns>true if time has elapse and active event, else other wise</returns>
    protected bool AssetTime(float deltaTime)
    {
        this._currentTime += deltaTime;
        if(this._currentTime >= this._timer)
        {
            EventTrigger();
            return true;
        }
        return false;
    }

    /// <summary>
    /// reset current time defalt to 0f
    /// </summary>
    public void ResetTimer()
    {
        this._currentTime = 0f;
    }

    /// <summary>
    /// active event
    /// </summary>
    public void EventTrigger()
    {
        if(this._callBack == null)
        {
            Debug.Log("Event timer is null");
            return;
        }
        _callBack.Invoke();
    }

    /// <summary>
    /// set time init timer
    /// </summary>
    /// <param name="newTimer"> timer want assign </param>
    public void SetTimer(float newTimer)
    {
        this._timer = newTimer;
        if(_timer == 0)
        {
            _timer = 0.1f;
        }
    }
}

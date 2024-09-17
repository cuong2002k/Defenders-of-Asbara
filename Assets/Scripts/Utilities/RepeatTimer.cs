using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatTimer : Timer
{
    public RepeatTimer(float newTimer, Action callBack) : base(newTimer, callBack)
    {
    }

    /// <summary>
    /// reset time when time elapse
    /// </summary>
    /// <param name="deltaTime"> the time change last tick </param>
    /// <returns></returns>
    public override bool Tick(float deltaTime)
    {
        if(this.AssetTime(deltaTime))
        {
            this.ResetTimer();
            return true;
        }
        return false;
    }
}

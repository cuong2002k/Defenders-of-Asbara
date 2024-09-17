using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{

    private List<Timer> _timers = new List<Timer>();

    protected virtual void Update()
    {
        for(int i = _timers.Count - 1; i >= 0; i--)
        {
            if(_timers[i].Tick(Time.deltaTime))
            {
                // stop timer
            }
        }
    }

    protected void StartTimer(Timer timer)
    {
        if(this._timers.Contains(timer))
        {
            Debug.LogError("Timer is exits");
            return;
        }
        this._timers.Add(timer);
    }

    protected void PauseTimer(Timer timer)
    {
        if(this._timers.Contains(timer))
        this._timers.Remove(timer);
    }

    protected void StopTimer(Timer timer)
    {
        timer.ResetTimer();
        PauseTimer(timer);
    }
}

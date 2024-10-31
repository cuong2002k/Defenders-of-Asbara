using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverExtentision
{
  public class EventDispatcher : Singleton<EventDispatcher>
  {
    
    #region Field
    private Dictionary<EventID, Action<object>> _listeners = new Dictionary<EventID, Action<object>>();
    #endregion

    #region Unity logic
    protected override void OnDestroy() {
      if(_instance == this)
      {
        _instance = null;
        this.ClearAllListener();
      }
    }
    #endregion

    #region Add Listener, Post event, Remove Listener 
    /// <summary>
    /// Register to listener for eventID
    /// </summary>
    /// <param name="eventID">EventID that object want to listen</param>
    /// <param name="callback">callback will be invoike when this eventID be raised</param>
    public void RegisterListener(EventID eventID, Action<object> callback)
    {
      // checking params
      Common.Assert(callback != null, "Add Listener, event {0}, callback = null !!!", eventID.ToString());
      Common.Assert(eventID != EventID.none, "RegisterListener, event = none !!!");

      // check if listener exists in dictionary
      if(this._listeners.ContainsKey(eventID)){
        // add call back to our collection
        this._listeners[eventID] += callback;
      }
      else{
        // add new pair key and event
        this._listeners.Add(eventID,null);
        this._listeners[eventID] += callback;
      }
    }

    /// <summary>
    /// Posts the event. This will notify all listener that register for this event 
    /// </summary>
    /// <param name="eventID">Event ID</param>
    /// <param name="param">parameter can be enything (struct, class, value, ...), Listener will make a cast to get the data</param>
    public void PostEvent(EventID eventID, object param = null)
    {
      // stop if not found event
      if(!this._listeners.ContainsKey(eventID))
      {
        Common.Log("No Listener for event {0}", eventID);
        return;
      }

      // post events
      var callback = this._listeners[eventID];
      if(callback != null)
      {
        callback(param);
      }
      else{
        Common.Log("Post event {0}, but no listener remain, Remove this key ", eventID);
        this._listeners.Remove(eventID);
      }
    }

    public void RemoveListener(EventID eventID, Action<object> callback)
    {
      // check params
      Common.Assert(callback != null, "RemoveListener, event {0}, callback = null !!", eventID.ToString());
      Common.Assert(eventID != EventID.none, "AddListener, event = null!");

      if(_listeners.ContainsKey(eventID))
      {
        _listeners[eventID] -= callback;
      }
      else
      {
        Common.Warning(false, "RemoveListener, not found key" + eventID.ToString());
      }
    }
    /// <summary>
    /// Clear all listener
    /// </summary>
    public void ClearAllListener()
    {
      this._listeners.Clear();
    }

    #endregion
  }

  public static class EventDispatcherExtension
  {
    public static void RegisterListener(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
      EventDispatcher.Instance.RegisterListener(eventID, callback);
    }

    public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param = null)
    {
      EventDispatcher.Instance.PostEvent(eventID, param);
    }
    

    public static void RemoveListener(this MonoBehaviour listener, EventID eventID, Action<object> callback){
      EventDispatcher.Instance.RemoveListener(eventID, callback);
    }


  }
}


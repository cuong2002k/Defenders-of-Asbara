using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T _instance;

    public static T Instance {
        get{
          if(_instance == null)
          {
            Common.Log("Create singleton of component");
            GameObject instance = new GameObject();
            instance.AddComponent<T>();
            _instance = instance.GetComponent<T>();
          }
          return _instance;
        }
    }

    public bool HasInstance() => _instance != null;

    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = (T) this;
        }
        else{
            Destroy(this.gameObject);
        }

    }

    protected virtual void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentHelper<T> : MonoBehaviour where T : Component
{
    private T component;

    public T Component
    {
        get
        {
            if (component == null)
            {
                component = GetComponent<T>();
            }
            return component;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewBase : MonoBehaviour
{
    public abstract void Initialize();
    public virtual void Show() => this.gameObject.SetActive(true);
    public virtual void Hide() => this.gameObject.SetActive(false);
}

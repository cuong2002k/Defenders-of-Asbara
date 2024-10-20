using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAble : MonoBehaviour
{
  [SerializeField] private int _capacity;
  public int Capacity => _capacity;
  private void OnDisable() {
      
  }
}

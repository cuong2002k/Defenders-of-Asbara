using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : TowerBase
{
  protected override void Shoot()
  {
    Common.Log("Attack");
  }
}

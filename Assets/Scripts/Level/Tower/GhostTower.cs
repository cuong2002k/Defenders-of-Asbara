using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTower : MonoBehaviour
{
  [SerializeField] private Material _valid;
  [SerializeField] private Material _inValid;
  private MeshRenderer[] meshRenderer;
  private void Start() {
    meshRenderer = GetComponentsInChildren<MeshRenderer>();
  }

  private void Update() {
    if(PlacementSystem.Instance.CanPlacement)
    {
      SetMaterial(_valid);
    }
    else{
      SetMaterial(_inValid);
    }
  }

  private void SetMaterial(Material material)
  {
    for(int i = 0; i < meshRenderer.Length; i++)
      {
        meshRenderer[i].material = material;
      }
  }
}

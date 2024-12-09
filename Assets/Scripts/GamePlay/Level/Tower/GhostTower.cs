using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostTower : MonoBehaviour, IPoolAble
{
    // material showing when valid
    [SerializeField] private Material _valid;
    // material showing when invalid
    [SerializeField] private Material _inValid;
    // check layer not allow building
    [SerializeField] private LayerMask _layerMask;
    // radius check
    [SerializeField] private float _radius;
    // all mesh use in model
    private MeshRenderer[] meshRenderer;
    private bool _canPlace = false;
    public bool CanPlace => this._canPlace;
    private void Awake()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        _canPlace = !CheckCollision();
        if (PlacementSystem.Instance.CanPlacement && !CheckCollision())
        {
            SetMaterial(_valid);
        }
        else
        {
            SetMaterial(_inValid);
        }
    }

    private void SetMaterial(Material material)
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].material = material;
        }
    }

    private bool CheckCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _radius, _layerMask);
        return colliders.Length > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(this.transform.position, _radius);
    }

    public void OnSpawn()
    {
        this.SetMaterial(this._valid);
    }

    public void OnDespawn()
    {
        PoolAble.TryReturn(this.gameObject);
    }
}

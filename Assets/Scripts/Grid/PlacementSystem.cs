using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObserverExtentision;
using UnityEngine;

public class PlacementSystem : Singleton<PlacementSystem>
{
    [Header("Component")]
    private InputManager _inputManager;
    private Grid _grid;
    private PathFinding _pathFiding;

    [Header("Placement")]
    public TowerBase _placement;
    private GameObject _ghostTower;
    private bool _isBuilding;
    private bool _canPlacement;
    public bool CanPlacement => _canPlacement;

    private void Start()
    {
      InitComponent();
      InitEvent();
    }

    private void InitComponent()
    {
       _inputManager = this.transform.parent.GetComponentInChildren<InputManager>();
       _pathFiding = this.transform.parent.GetComponentInChildren<PathFinding>();
       _grid = this.transform.parent.GetComponentInChildren<Grid>();
    }

    private void InitEvent()
    {
      this.RegisterListener(EventID.StartBuilding, (param) => this.SetTowerPlace(param as TowerBase));
    }

    private void Update()
    {
        if (_inputManager.EscButton)
        {
            _isBuilding = false;
            _ghostTower.SetActive(false);
            UpdatePath();
        }

        if (!_isBuilding) return;
        Node currentNode = GetCurrentNodeWithMouse();
        this._ghostTower.transform.position = currentNode.GetWorldPosition();
        UpdatePath();

        _canPlacement = _pathFiding.CanMove ;
        if (_canPlacement && !_inputManager.IsPointerOverUI() && _inputManager.LeftMouseBuuton)
        {
            PlaceObject(currentNode.GetWorldPosition());
        }

        if(_inputManager.RightMouseButton)
        {
          RotationPlacement();
        }
    }

    private void PlaceObject(Vector3 position)
    {
        GameObject objectPlacement = PoolManager.Instance.GetObjectPool(this._placement.TowerData.TowerPrefabs.gameObject);
        // set default material
        objectPlacement.transform.position = position;
        objectPlacement.transform.rotation = _ghostTower.transform.rotation;
        // reset building
        this._isBuilding = false;
        // hide object place
        this._ghostTower.SetActive(false);
        //Update Eneny Path
        this.PostEvent(EventID.OnUpdatePath, PathManager.Instance.Paths);

    }

    private void RotationPlacement()
    {
      this._ghostTower.transform.Rotate(0f, 90f, 0f);
    }

    private Node GetCurrentNodeWithMouse()
    {
        Vector3 gridPos = this._inputManager.GetSelectedMapPosition();
        Node currentNode = _grid.NodeFromWorldPoint(gridPos);
        return currentNode;
    }

    private void UpdatePath()
    {
      PathManager.Instance.UpdatePath();
    }

    public void SetTowerPlace(TowerBase tower)
    {
      ResetPlacementObject();
      _isBuilding = true;
      this._placement = tower;
      this._ghostTower = PoolManager.Instance.GetObjectPool(tower.TowerData.GhostTower.gameObject);
      _ghostTower.SetActive(true);
    }

    private void ResetPlacementObject()
    {
      if(this._ghostTower != null){
        this._ghostTower.SetActive(false);
        this._ghostTower = null;
      }
      if(this._placement != null) this._placement = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefenderOfAsbara.UI;
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
    private GhostTower _ghostTower;
    private bool _isBuilding;
    public bool IsBuilding => _isBuilding;
    private bool _canPlacement;
    public bool CanPlacement => _canPlacement;

    RadiusVisualized _radiusVisualized;

    protected override void Awake()
    {
        base.Awake();
        InitComponent();
    }

    protected void Start()
    {
        InitEvent();
        this._radiusVisualized = UIManager.GetView<RadiusVisualized>() as RadiusVisualized;
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

    private void OnDisable()
    {
        this.RemoveListener(EventID.StartBuilding, (param) => this.SetTowerPlace(param as TowerBase));
    }

    private void Update()
    {
        CloseBuilding();
        BuildingProcess();
    }

    private void BuildingProcess()
    {
        if (!_isBuilding || this._ghostTower == null) return;
        Node currentNode = GetCurrentNodeWithMouse();
        this._ghostTower.transform.position = currentNode.GetWorldPosition();
        UpdatePathWhenView();
        _canPlacement = _pathFiding.CanMove;
        if (_canPlacement && !_inputManager.IsPointerOverUI() && _inputManager.LeftMouseBuuton && this._ghostTower.CanPlace)
        {
            FinishPlacement(currentNode.GetWorldPosition());
        }

        if (_inputManager.RightMouseButton)
        {
            RotationPlacement();
        }
    }

    /// <summary>
    /// Place object in grid
    /// </summary>
    /// <param name="position">Location will be spawned</param>
    private void FinishPlacement(Vector3 position)
    {
        TowerBase objectPlacement = PoolAble.TryGetPool<TowerBase>(_placement.TowerConfig.TowerPrefabs.gameObject);
        objectPlacement.OnSpawn();
        // set default material
        objectPlacement.transform.position = position;
        objectPlacement.transform.rotation = _ghostTower.transform.rotation;
        // reset building
        this._isBuilding = false;
        LevelManager.Instance.MinusCoin(this._placement.GetCurrentCostLevel);
        // hide object place
        this._ghostTower.OnDespawn();
        //Update Eneny Path
        this.UpdatePathWhenPlacement();
        this.PostEvent(EventID.OnUpdatePath, PathManager.Instance.Paths);


    }

    /// <summary>
    /// rotation placement object with 90 angle follow y
    /// </summary>
    private void RotationPlacement()
    {
        this._ghostTower.transform.Rotate(0f, 90f, 0f);
    }

    /// <summary>
    /// through mouse input get current node in grid
    /// </summary>
    /// <returns></returns>
    private Node GetCurrentNodeWithMouse()
    {
        Vector3 gridPos = this._inputManager.GetSelectedMapPosition();
        Node currentNode = _grid.NodeFromWorldPoint(gridPos);
        return currentNode;
    }

    /// <summary>
    /// Update new path when user place object in world
    /// </summary>
    private void UpdatePathWhenPlacement()
    {
        PathManager.Instance.UpdatePathPrimary();
    }

    /// <summary>
    /// Update path view
    /// </summary>
    private void UpdatePathWhenView()
    {
        PathManager.Instance.UpdatePathView();
    }

    private void UpdatePathWhenView(Node current, Node old)
    {
        PathManager.Instance.UpdatePathView(current, old);
    }

    /// <summary>
    /// Set tower data when click button in UI
    /// </summary>
    /// <param name="tower"></param>
    public void SetTowerPlace(TowerBase tower)
    {
        ResetPlacementObject();
        _isBuilding = true;
        this._placement = tower;
        GhostTower ghostInstance = PoolAble.TryGetPool<GhostTower>(tower.TowerConfig.GhostTower.gameObject);
        if (ghostInstance != null)
        {
            this._ghostTower = ghostInstance;
            if (_radiusVisualized != null)
            {
                this._radiusVisualized.SetupRadiusVisual(ghostInstance.transform, tower.CurrentTargetRange, tower.GetTargetRangeColor);
            }
        }
        else
        {
            Common.LogWarning("Ghost Tower component not found in ", tower);
        }

    }

    /// <summary>
    /// Reset default placement building
    /// </summary>
    private void ResetPlacementObject()
    {
        if (this._ghostTower != null)
        {
            this._radiusVisualized.Hide();
            this._ghostTower.OnDespawn();
            this._ghostTower = null;

        }
        if (this._placement != null) this._placement = null;
    }

    /// <summary>
    /// Stop building when click esc
    /// </summary>
    private void CloseBuilding()
    {
        if (_inputManager.EscButton)
        {
            _isBuilding = false;
            _ghostTower.OnDespawn();
            UpdatePathWhenView();
        }
    }

    public Vector3 GetMouseToGridPosition()
    {
        Vector3 gridPos = this._inputManager.GetSelectedMapPosition();
        Node currentNode = _grid.NodeFromWorldPoint(gridPos);
        return currentNode.GetWorldPosition();
    }
}

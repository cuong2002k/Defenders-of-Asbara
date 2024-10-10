using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementSystem : Singleton<PlacementSystem>
{
    [Header("Component")]
    private InputManager _inputManager;
    private Grid _grid;
    private PathFinding _pathFiding;



    [Header("Placement")]
    public GameObject Placement;
    private bool _isBuilding;
    public Material _validate;
    public Material _invalid;
    private Material _current;
    private Renderer _placementMaterial;

    private void Start()
    {
        _inputManager = this.transform.parent.GetComponentInChildren<InputManager>();
        _pathFiding = this.transform.parent.GetComponentInChildren<PathFinding>();
        _grid = this.transform.parent.GetComponentInChildren<Grid>();
        _placementMaterial = Placement.GetComponent<Renderer>();
        _current = _placementMaterial.sharedMaterial;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isBuilding = false;
            Placement.SetActive(false);
        }

        if (!_isBuilding) return;
        Node currentNode = GetCurrentNodeWithMouse();
        this.Placement.transform.position = currentNode.GetWorldPosition();
        UpdatePath(currentNode);

        bool canPlacement = CheckPlacement();
        if (Input.GetKeyDown(KeyCode.Mouse0) && canPlacement)
        {
            PlaceObject(currentNode.GetWorldPosition());
        }
    }

    private void PlaceObject(Vector3 position)
    {
        GameObject objectPlacement = Instantiate(Placement, position, Quaternion.identity);
        // set default material
        // objectPlacement.GetComponent<Renderer>().sharedMaterial = _current;
        // reset building
        this._isBuilding = false;
        // hide object place
        this.Placement.SetActive(false);
        //Update Eneny Path
        LevelManager.Instance.UpdateEnemyPath();
    }

    private Node GetCurrentNodeWithMouse()
    {
        Vector3 gridPos = this._inputManager.GetSelectedMapPosition();
        Node currentNode = _grid.NodeFromWorldPoint(gridPos);
        return currentNode;
    }

    private void UpdatePath(Node currentNode)
    {
      PathManager.Instance.UpdatePath(currentNode);
    }

    private bool CheckPlacement()
    {
        if (_pathFiding.CanMove)
        {
            _placementMaterial.sharedMaterial = _validate;
            return true;
        }
        else
        {
            _placementMaterial.sharedMaterial = _invalid;
            return false;
        }
    }

    public void SetTowerPlace(TowerBase tower)
    {
      _isBuilding = true;
      Placement.SetActive(true);
      this.Placement = Instantiate(tower, this.transform.position, Quaternion.identity).gameObject;
    }


}

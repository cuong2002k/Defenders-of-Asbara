using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [Header("Component")]
    private InputManager _inputManager;
    private LevelManager _levelManager;
    private Grid _grid;
    private PathFinding _pathFiding;
    

    [Header("Placement")]
    public GameObject Placement;
    private bool _isBuilding;
    public Material _validate;
    public Material _invalid;
    private Renderer _placementMaterial;

    private void Start()
    {
        _inputManager = this.transform.parent.GetComponentInChildren<InputManager>();
        _pathFiding = this.transform.parent.GetComponentInChildren<PathFinding>();
        _grid = this.transform.parent.GetComponentInChildren<Grid>();
        _levelManager = LevelManager.instance;
        _placementMaterial = Placement.GetComponent<Renderer>();
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.B))
        {
            _isBuilding = true;
            Placement.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isBuilding = false;
            Placement.SetActive(false);
        }

        if (!_isBuilding) return;
        Vector3 gridPos = this._inputManager.GetSelectedMapPosition();
        Node currentNode = _grid.NodeFromWorldPoint(gridPos);
        this.Placement.transform.position = currentNode.GetWorldPosition();
        _grid.UpdateGridNode(currentNode);
        _pathFiding.StartFindingPath(_levelManager.StartPoint, _levelManager.EndPoint);

        if (_pathFiding.canMove)
        {
            _placementMaterial.sharedMaterial = _validate;
            // Intitance object 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlaceObject(currentNode.GetWorldPosition());
            }
        }
        else
        {
            _placementMaterial.sharedMaterial = _invalid;
        }

    }

    private void PlaceObject(Vector3 position)
    {
        Instantiate(Placement, position, Quaternion.identity);
        this._isBuilding = false;
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float _maxDistanceRay = 100f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _towerLayer;
    private Vector3 lastPosition;

    private bool _rightMouseButton;
    public bool RightMouseButton => _rightMouseButton;
    private bool _leftMouseButton;
    public bool LeftMouseBuuton => _leftMouseButton;
    private bool _escButton;
    public bool EscButton => _escButton;

    Vector3 _mousePosition;
    private void Update()
    {
        _escButton = Input.GetKeyDown(KeyCode.Escape);
        _leftMouseButton = Input.GetKeyDown(KeyCode.Mouse0);
        _rightMouseButton = Input.GetKeyDown(KeyCode.Mouse1);
        _mousePosition = Input.mousePosition;
        TryGetTower();
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public Vector3 GetSelectedMapPosition()
    {
        _mousePosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistanceRay, _layerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }

    private void TryGetTower()
    {
        if (_leftMouseButton)
        {
            UIManager.Instance.TowerUI.TrySelectTower(_mousePosition);
        }
    }
}
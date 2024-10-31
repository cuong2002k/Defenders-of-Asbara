using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float _maxDistanceRay = 100f;
    [SerializeField] private LayerMask _layerMask;
    private Vector3 lastPosition;

    private bool _rightMouseButton;
    public bool RightMouseButton => _rightMouseButton;
    private bool _leftMouseButton;
    public bool LeftMouseBuuton => _leftMouseButton;
    private bool _escButton;
    public bool EscButton => _escButton;
    private void Update()
    {
      _escButton = Input.GetKeyDown(KeyCode.Escape);
      _leftMouseButton = Input.GetKeyDown(KeyCode.Mouse0);
      _rightMouseButton = Input.GetKeyDown(KeyCode.Mouse1);
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mouseInput = Input.mousePosition;
        mouseInput.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mouseInput);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDistanceRay, _layerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
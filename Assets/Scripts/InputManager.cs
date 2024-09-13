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
    public Action OnClicked, OnRotation, OnExit;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRotation?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
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

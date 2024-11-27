using System.Collections;
using System.Collections.Generic;
using DefenderOfAsbara.UI;
using ObserverExtentision;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerUI : MonoBehaviour
{
    private TowerBase _currentTowerBase;
    private RaycastHit _hit;
    private Ray ray;
    [SerializeField] private LayerMask _towerLayer;

    [SerializeField] private TowerInfor _towerInfor;

    private float _maxDistanceRay = 100;

    private Camera _gameCamera;

    private EventSystem _eventSystem;

    private RadiusVisualized _radiusVisualized;

    private void Awake()
    {
        _towerInfor = GetComponentInChildren<TowerInfor>();
    }

    private void Start()
    {
        _gameCamera = Camera.main;
        _eventSystem = EventSystem.current;
        _radiusVisualized = UIManager.GetView<RadiusVisualized>() as RadiusVisualized;

    }

    public void TrySelectTower(Vector3 worldPosition)
    {
        if (PlacementSystem.Instance.IsBuilding || _eventSystem.IsPointerOverGameObject())
        {
            return;
        }

        bool hasHit = Physics.Raycast(Camera.main.ScreenPointToRay(worldPosition), out _hit, _maxDistanceRay, _towerLayer);
        if (hasHit)
        {
            _currentTowerBase = _hit.collider.GetComponent<TowerBase>();
            if (_currentTowerBase != null)
            {
                SetTowerVisualRadius(_currentTowerBase);
                this._towerInfor.Initialize(_currentTowerBase);
                this._towerInfor.Show();
                SetUpPosition(this._towerInfor.gameObject, _currentTowerBase.transform.position);
            }
            else
            {
                if (_radiusVisualized != null)
                {
                    _radiusVisualized.Hide();
                }
                this._towerInfor.Hide();
            }
        }
        else
        {
            if (_radiusVisualized != null)
            {
                _radiusVisualized.Hide();
            }

            this._towerInfor.Hide();

        }
    }

    private void SetTowerVisualRadius(TowerBase towerBase)
    {
        if (towerBase == null)
        {
            Common.LogWarning("TowerBase is not null {0}", towerBase);
            return;
        }
        if (this._radiusVisualized != null)
        {
            this._radiusVisualized.SetupRadiusVisual(towerBase.transform, towerBase.CurrentTargetRange, towerBase.GetTargetRangeColor);
        }
    }

    private void SetUpPosition(GameObject objectNeedSet, Vector3 position)
    {
        Vector3 point = _gameCamera.WorldToScreenPoint(position);
        point.y -= 100f;
        point.z = 0;
        objectNeedSet.transform.position = point;
    }


}

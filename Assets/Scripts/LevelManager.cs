using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private LineRenderer _linerenderer;

    [Header("Wave infor")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    public Vector3 StartPoint => _startPoint.transform.position;
    public Vector3 EndPoint => _endPoint.transform.position;
    [SerializeField] private Vector3[] _path;
    [Header("Enemy")]
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _enemyCount = 5;
    [SerializeField] private float _enemyPerSecond = 2;
    private float _enemyTimer = 0;

    [SerializeField] private Transform _enemyContainer;

    private void Awake()
    {
        instance = this;
    }

    


}

using System;
using System.Collections;
using System.Collections.Generic;
using ObserverExtentision;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamage
{
    [Header("Enemy Data")]
    [SerializeField] private EnemyData _enemyData;
    private int _speed;
    public int Speed => _speed;
    [SerializeField] private float _health;
    private float _maxHealth;
    private int _coinReceiver;

    [Header("Component")]
    private Rigidbody _enemyRB;
    public Rigidbody EnemyRB => _enemyRB;

    [Header("Path moving")]
    private List<Node> _agent = new List<Node>();
    public List<Node> Agent => _agent;
    private int _pathIndex = 0;
    public int PathIndex => _pathIndex;

    private ProcessBar _processBar;

    public StateMachine StateMachine { get; private set; }

    public MoveState _moveState;

    private void Awake()
    {
        this._processBar = GetComponentInChildren<ProcessBar>();
        this._enemyRB = GetComponent<Rigidbody>();
    }

    public event Action<Transform> OnDie;
    private void Start()
    {
        LevelManager.Instance.AddEnemyNumber();
        Initialized();
    }

    private void Initialized()
    {
        InitStats();
        _processBar.SetProcessBar(this._health / this._maxHealth);
        this.RegisterListener(EventID.OnUpdatePath, (param) => this.SetPath(param as List<Node>));

        //stateMachine
        _moveState = new MoveState();
        StateMachine = new StateMachine(_moveState, this);
    }

    private void InitStats()
    {
        this._speed = this._enemyData.Speed;
        this._health = this._enemyData.Health;
        this._coinReceiver = this._enemyData.CoinReceiver;
        this._maxHealth = this._health;
    }

    /// <summary>
    /// Set path moving when path control change
    /// </summary>
    /// <param name="agent"></param>
    public void SetPath(List<Node> agent)
    {
        this._agent = agent;
    }

    void Update()
    {
        if (_pathIndex >= _agent.Count)
        {
            OnRemove();
            return;
        }

        this.StateMachine.CurrentState.OnUpdate(this);

    }

    public void NextIndex()
    {
        this._pathIndex++;
    }

    private void FixedUpdate()
    {
        this.StateMachine.CurrentState.OnFixedUpdate(this);
    }

    /// <summary>
    /// Take dame from tower 
    /// </summary>
    /// <param name="damage">Dame taken</param>
    public void TakeDamage(int damage)
    {
        int dameFinal = Mathf.Max(damage - this._enemyData.Armor, 1);
        _health -= dameFinal;
        _processBar.SetProcessBar(this._health / this._maxHealth);
        if (_health <= 0)
        {
            LevelManager.Instance.AddCoin(this._coinReceiver);
            LevelManager.Instance.MinusEnemyNumber();
            this.OnDie?.Invoke(this.transform);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Remove event when enemy die or disable
    /// </summary>
    private void OnDisable()
    {
        this.RemoveListener(EventID.OnUpdatePath, (param) => this.SetPath(param as List<Node>));
    }

    private void OnRemove()
    {
        LevelManager.Instance.MinusEnemyNumber();
        LevelManager.Instance.AttackHomeBase(this._enemyData.DamageToDefense);
        this.OnDie?.Invoke(this.transform);
        Destroy(this.gameObject);
    }

}

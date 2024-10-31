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
  [SerializeField] private int _health;
  private int _coinReceiver;

  [Header("Component")]
  private Rigidbody _enemyRB;

  [Header("Path moving")]
  private List<Node> _agent = new List<Node>();
  private int _pathIndex = 0;

  [Header("VFX")]
  [SerializeField] private GameObject _hitEffect;

  private void Start()
  {
    LevelManager.Instance.AddEnemyNumber();
    this.RegisterListener(EventID.OnUpdatePath, (param) => this.SetPath(param as List<Node>));
    this._enemyRB = GetComponent<Rigidbody>();
    this._speed = this._enemyData.Speed;
    this._health = this._enemyData.Health;
    this._coinReceiver = this._enemyData.CoinReceiver;
    
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
      LevelManager.Instance.MinusEnemyNumber();
      LevelManager.Instance.AddCoin(this._coinReceiver);
      Destroy(this.gameObject);
      return;
    }

    Vector3 positionMove = _agent[_pathIndex].GetWorldPosition();
    float distance = Vector3.Distance(this.transform.position, positionMove);
    if (distance <= 0.1f)
    {
      _pathIndex++;
    }

  }

  private void FixedUpdate() 
  {
    Movement();
  }

  private void Movement()
  {
    if (_pathIndex >= _agent.Count) return;
    Vector3 positionMove = _agent[_pathIndex].GetWorldPosition();
    Vector3 direction = (positionMove - this.transform.position).normalized;
    Rotation(direction);
    this._enemyRB.velocity = direction * this._speed;
  }

  private void Rotation(Vector3 direction)
  {
    Quaternion quaternion = Quaternion.LookRotation(direction);
    this.transform.rotation = quaternion;
  }

  /// <summary>
  /// Take dame from tower 
  /// </summary>
  /// <param name="damage">Dame taken</param>
  public void TakeDamage(int damage)
  {
    _health -= damage;
    GameObject hitInstance = Instantiate(this._hitEffect, transform.position, Quaternion.identity);
    Destroy(hitInstance, 2f);
    if(_health <= 0) 
    {
      LevelManager.Instance.AddCoin(this._coinReceiver);
      LevelManager.Instance.MinusEnemyNumber();
      Destroy(this.gameObject);
    }
  }
  
  /// <summary>
  /// Remove event when enemy die or disable
  /// </summary>
  private void OnDisable() {
    this.RemoveListener(EventID.OnUpdatePath, (param) => this.SetPath(param as List<Node>));
  }
}

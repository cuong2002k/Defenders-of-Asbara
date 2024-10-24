using System.Collections;
using System.Collections.Generic;
using ObserverExtentision;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
  [SerializeField] private EnemyData _enemyData;
  private int _pathIndex = 0;
  private int _speed;
  private int _health;
  private int _coinReceiver;
  private List<Node> _agent = new List<Node>();
  [SerializeField] private GameObject _hitEffect;

  private void Start()
  {
    LevelManager.Instance.AddEnemyNumber();
    this.RegisterListener(EventID.OnUpdatePath, (param) => this.SetPath(param as List<Node>));

    this._speed = this._enemyData.Speed;
    this._health = this._enemyData.Health;
    this._coinReceiver = this._enemyData.CoinReceiver;
    
  }

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
    this.transform.position = Vector3.MoveTowards(this.transform.position, positionMove, _speed * Time.deltaTime);

  }

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

  private void OnDisable() {
    this.RemoveListener(EventID.OnUpdatePath);
  }
}

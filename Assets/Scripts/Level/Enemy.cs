using System.Collections;
using System.Collections.Generic;
using ObserverExtentision;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
  private List<Node> _agent = new List<Node>();
  private int _pathIndex = 0;
  private float _speed = 3f;
  private float health = 3f;
  [SerializeField] private GameObject _hitEffect;

  private void Start()
  {
    LevelManager.Instance.AddEnemyNumber();
    this.RegisterListener(EventID.OnUpdatePath, (param) => this.SetPath(param as List<Node>));
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
    health -= damage;
    GameObject hitInstance = Instantiate(this._hitEffect, transform.position, Quaternion.identity);
    Destroy(hitInstance, 2f);
    if(health <= 0) Destroy(this.gameObject);
  }

  private void OnDisable() {
    this.RemoveListener(EventID.OnUpdatePath);
  }
}

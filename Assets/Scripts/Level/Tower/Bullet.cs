using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private float _speed = 20f;
    private Rigidbody _rigidbody;

    private void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetTarget(Transform target)
    {
      this._target = target;
    }

    private void Update()
    {
      if(this._target == null) Destroy(this.gameObject);

        if(Vector3.Distance(this.transform.position, _target.position) <= 0.1f)
        {
          Destroy(this.gameObject);
        }

    }


    private void FixedUpdate()
    {
      if(this._target == null) return;

      Vector3 direction = (this._target.position - this.transform.position).normalized;
        _rigidbody.velocity = direction * _speed;
    }
}

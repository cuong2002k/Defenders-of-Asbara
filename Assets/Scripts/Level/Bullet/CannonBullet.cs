using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CannonBullet : BulletBase
{

  protected override void Start()
  {
    base.Start();
    _rigidbody.velocity = CaculatorVelocity() * this._speed;
  }

  Vector3 CaculatorVelocity()
  {
    Vector3 current = this.transform.position;
    Vector3 target = this._target.position;
    return target - current;
  }

  private void OnTriggerEnter(Collider other)
  {
    IDamage damage = other.GetComponent<IDamage>();
    if (damage != null)
    {
      damage.TakeDamage(_damage);
      Destroy(this.gameObject);
    }
  }

}

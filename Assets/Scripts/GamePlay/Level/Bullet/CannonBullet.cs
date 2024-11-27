using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CannonBullet : BulletBase
{
    TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }
    public override void SetTarget(Transform target)
    {
        base.SetTarget(target);
        if (this._rigidbody == null)
        {
            this.InitCompnent();
        }
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
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
    }

}

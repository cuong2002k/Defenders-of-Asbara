using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CannonBullet : BulletBase
{
    TrailRenderer trailRenderer;
    [SerializeField] private GameObject _muzzleEffect;

    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    public override void Initialized(Transform target, int damage)
    {
        base.Initialized(target, damage);
        if (this._rigidbody == null)
        {
            this.InitCompnent();
        }

        _rigidbody.velocity = CaculatorVelocity() * this._speed;
    }

    Vector3 CaculatorVelocity()
    {
        if (this._target == null) return Vector3.one;
        Vector3 current = this.transform.position;
        Vector3 target = this._target.position;
        return target - current;
    }



    private void OnTriggerEnter(Collider other)
    {
        IDamage damage = other.GetComponent<IDamage>();
        if (damage == null) return;

        if (this._hitEffect != null)
        {
            Vector3 contactPoint = other.ClosestPoint(this.transform.position);

            GameObject hitFX = PoolAble.TryGetPool(this._hitEffect);
            hitFX.transform.position = contactPoint;
        }

        damage.TakeDamage(_damage);
        this.OnDespawn();
    }

    public override void OnDespawn()
    {
        trailRenderer.Clear();
        base.OnDespawn();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        if (this._muzzleEffect == null) return;
        // GameObject muzzle = PoolAble.TryGetPool(this._muzzleEffect);
        // muzzle.transform.position = this.transform.position;
        Vector3 bulletDirection = CaculatorVelocity();
        this.transform.rotation = Quaternion.LookRotation(bulletDirection);
        // muzzle.transform.rotation = Quaternion.LookRotation(bulletDirection);


    }

}

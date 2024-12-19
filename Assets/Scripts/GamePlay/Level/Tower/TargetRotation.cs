using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetRotation : MonoBehaviour
{
    [Header("Component")]
    Targetter _targetAble;

    [Header("Turret")]
    [SerializeField] protected Transform _turret; // ref with turret of tower to rotation
    public Transform Turret => this._turret;
    protected float _rotationSpeed; // speed rotation when have target

    #region Unity Logic
    private void Awake()
    {
        this._targetAble = GetComponentInChildren<Targetter>();
    }

    private void Update()
    {
        Transform target = _targetAble.GetFirstTarget();
        if (target != null)
        {
            LookTarget(target);
        }
    }

    #endregion Unity Logic

    public void Initialize(float rotationSpeed, Transform turret)
    {
        this._rotationSpeed = rotationSpeed;
        this._turret = turret;
    }

    /// <summary>
    /// Rotation following enemy in range
    /// </summary>
    /// <param name="target"> the turret will rotate accordingly </param>
    private void LookTarget(Transform target)
    {
        Vector3 direction = this.GetDirection(target);
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_turret.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        _turret.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private Vector3 GetDirection(Transform target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 current = this._turret.transform.position;
        Vector3 direction = targetPosition - current;

        return direction;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform CurrentTranform => this.transform;

    private Rigidbody _rigidBody;
    public Rigidbody Rigidbody => _rigidBody;

    private void Awake()
    {
        this._rigidBody = GetComponent<Rigidbody>();
    }
}

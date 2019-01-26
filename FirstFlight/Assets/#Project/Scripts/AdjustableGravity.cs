using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdjustableGravity : MonoBehaviour
{
    public float _gravityFactor;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidBody.AddForce(Physics.gravity * _gravityFactor);
    }
}

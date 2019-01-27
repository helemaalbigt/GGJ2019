using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdjustableGravity : MonoBehaviour
{
    public float _gravMin;
    public float _gravMax;
    public float _optimalSpeed;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidBody.AddForce(Physics.gravity * _gravMin);
    }

    private float GetGravityFactor()
    {
       // return Utils.MapValue(_rigidBody.velocity.magnitude, 0, _optimalSpeed, _gravMin, _gravMax);
        return _gravMin;
    }
}

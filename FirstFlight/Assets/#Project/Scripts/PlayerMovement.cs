using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float forwardMultiplier;
    public float liftMultiplier;
    public bool applyDrift = true;
    public float driftMultiplier;
    public float minSpeedForDrift = .5f;

    [Space(15)]
    public Rigidbody _rigidBody;
    public FlapData _leftFlap;
    public FlapData _rightFlap;
    public WingAngle _wingAngle;

    void FixedUpdate()
    {
        ApplyLift();
        ApplyForwardForce();
        if(applyDrift)
            ApplyDrift();

        ConstrainSpeed();
    }

    private void ApplyLift()
    {
        _rigidBody.AddRelativeForce(Vector3.up.normalized * AverageFlapForce() * liftMultiplier, ForceMode.Acceleration);
    }

    private void ApplyForwardForce()
    {
        _rigidBody.AddRelativeForce(Vector3.forward.normalized * AverageFlapForce() * forwardMultiplier, ForceMode.Acceleration);
    }

    private float AverageFlapForce()
    {
        return (_leftFlap.downForce + _rightFlap.downForce) / 2f;
    }

    private void ConstrainSpeed()
    {
        if (_rigidBody.velocity.magnitude > maxSpeed)
        {
            _rigidBody.velocity = _rigidBody.velocity.normalized * maxSpeed;
        }
    }

    private void ApplyDrift()
    {
        if (_rigidBody.velocity.magnitude < minSpeedForDrift)
            return;
        _rigidBody.AddRelativeForce(Vector3.right.normalized * _wingAngle.angle * driftMultiplier);
    }
}

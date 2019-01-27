using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float forwardMultiplier;
    public bool applyLift =  true;
    public float liftMultiplier;
    public bool applyDrift = true;
    public float driftMultiplier;
    public float minSpeedForDrift = .5f;
    public float minHandDist = .8f;
    public float maxHandDist = 1.2f;

    [Space(15)]
    public Rigidbody _rigidBody;
    public FlapData _leftFlap;
    public FlapData _rightFlap;
    public WingAngle _wingAngle;

    void FixedUpdate()
    {
        ApplyForwardForce();

        if (applyLift)
            ApplyLift();
        if (applyDrift)
            ApplyDrift();

        ConstrainSpeed();
    }

    private void ApplyLift()
    {
        _rigidBody.AddRelativeForce(Vector3.up.normalized * AverageFlapForce() * liftMultiplier * HandDistanceModifier(), ForceMode.Acceleration);
    }

    private void ApplyForwardForce()
    {
        _rigidBody.AddRelativeForce(Vector3.forward.normalized * AverageFlapForce() * forwardMultiplier * HandDistanceModifier(), ForceMode.Acceleration);
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

    private float HandDistanceModifier()
    {
        var dist = Vector3.Distance(_leftFlap.transform.position, _rightFlap.transform.position);
        return MapValue(dist, minHandDist, maxHandDist, 0, 1f);

    }

    private void ApplyDrift()
    {
        if (_rigidBody.velocity.magnitude < minSpeedForDrift || HandNotTracking(XRNode.LeftHand) || HandNotTracking(XRNode.RightHand))
            return;
        _rigidBody.AddRelativeForce(Vector3.right.normalized * _wingAngle.angle * driftMultiplier);
    }

    private float MapValue(float value, float minA, float maxA, float minB, float maxB)
    {
        float normal = Mathf.InverseLerp(minA, maxA, value);
        return Mathf.Lerp(minB, maxB, normal);

    }

    private bool HandNotTracking(XRNode hand)
    {
        return InputTracking.GetLocalPosition(hand) == Vector3.zero;
    }
}

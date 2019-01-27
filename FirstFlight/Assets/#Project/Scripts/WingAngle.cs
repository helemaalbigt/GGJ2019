using UnityEngine;
using UnityEngine.XR;

public class WingAngle : MonoBehaviour
{
    public float angle;

    [Space(25)]
    public Transform _leftHand;
    public Transform _rightHand;
    public float _minAngle;

    private Vector3 _handSpan;
    private Vector3 _handSpanProjected;
    private Vector3 _cross;
    private float _dot;
    private float _absAngle;

    void Update()
    {
        _handSpan = _rightHand.position - _leftHand.position;
        _handSpanProjected = Vector3.ProjectOnPlane(_handSpan, Vector3.up);
        _absAngle = Vector3.Angle(_handSpan, _handSpanProjected);
        _cross = Vector3.Cross(_handSpan, _handSpanProjected);
        _dot = Vector3.Dot(_cross, Vector3.forward);
        angle = _absAngle * Mathf.Sign(_dot);

        if (Mathf.Abs(angle) < _minAngle)
            angle = 0;
    }
}

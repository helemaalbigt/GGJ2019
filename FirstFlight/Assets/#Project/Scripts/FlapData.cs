using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR;

public class FlapData : MonoBehaviour
{
    public event Action onFlap;

    public float downForce;
    public float averageFlapSpeed;
    public XRNode hand;

    public int _speedSampleSize;

    [Space(25)] 
    private Vector3 _pos;
    private Vector3 _prevPos;
    private float _deltaY;
    private Stack<float> _flapSpeedLog = new Stack<float>();
    private float _downFlapDist;
    private bool _flapRegistered;

    private const float _cutoff = -0.005f;
    private float _minFlapDistance = .1f;

    void Start()
    {
        _prevPos = transform.position;
    }

    void Update()
    {
        if (InputDevices.GetDeviceAtXRNode(hand) == null)
            return;

        CalcDownForce();
        CalcFlapSpeed();
        UpdateDownFlapDistance();
    }

    private void CalcDownForce()
    {
        _pos = transform.localPosition;
        _deltaY = _pos.y - _prevPos.y;

        downForce = _deltaY < _cutoff ? Mathf.Abs(_deltaY) : 0;

        _prevPos = transform.localPosition;
    }

    private void CalcFlapSpeed()
    {
        _flapSpeedLog.Push(Mathf.Abs(_deltaY) / Time.deltaTime);

        if (_flapSpeedLog.Count > _speedSampleSize)
        {
            averageFlapSpeed = _flapSpeedLog.Take(_speedSampleSize).Sum() / _speedSampleSize;
        }
    }

    private void UpdateDownFlapDistance()
    {
        if (_deltaY < 0)
        {
            _downFlapDist += _deltaY;

            if (Mathf.Abs(_downFlapDist) > _minFlapDistance && !_flapRegistered)
            {
                FireFlap();
                _flapRegistered = true;
            }
        }
        else
        {
            _downFlapDist = 0;
            _flapRegistered = false;
        }
    }

    private void FireFlap()
    {
        if (onFlap != null)
            onFlap();
    }
}

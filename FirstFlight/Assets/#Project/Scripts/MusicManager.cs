using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource _mainLoop;
    public AudioSource _shittyLoop;

    [Space(25)]
    public bool _flapTempoEnabled;
    public FlapData _left;
    public FlapData _right;
    public SpeedMapValues _speedMapValues;

    private float _pitch;

    void Update()
    {
        if (_flapTempoEnabled)
        {
            _pitch = MapFlapSpeedToTempo();;
            _mainLoop.pitch = _pitch;
            _shittyLoop.pitch = _pitch;
        }
    }

    private float MaxFlapSpeed()
    {
        return Mathf.Max(_left.averageFlapSpeed, _right.averageFlapSpeed);
    }

    private float MapFlapSpeedToTempo()
    {
        return MapValue(MaxFlapSpeed(), _speedMapValues.minSpeed, _speedMapValues.crazySpeed, _speedMapValues.minPitch, _speedMapValues.maxPitch);
    }

    private float MapValue(float value, float minA, float maxA, float minB, float maxB)
    {
        float normal = Mathf.InverseLerp(minA, maxA, value);
        return Mathf.Lerp(minB, maxB, normal);

    }
}

[System.Serializable]
public class SpeedMapValues
{
    public float minPitch;
    public float maxPitch;
    public float minSpeed;
    public float crazySpeed;
}

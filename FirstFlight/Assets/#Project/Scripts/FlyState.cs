using System;
using UnityEngine;
using UnityEngine.UI;

public class FlyState : GameState
{
    public GroundDetector _groundDetector;
    public Text _distance;

    private bool _touchedGround;

    void OnEnable()
    {
        _groundDetector.onTouchedGround += TouchedGround;
    }

    void OnDisable()
    {
        _groundDetector.onTouchedGround -= TouchedGround;
        _touchedGround = false;
    }

    private void TouchedGround()
    {
        _touchedGround = true;
        _distance.text = Mathf.RoundToInt(_groundDetector.transform.position.z).ToString() + "m";
    }

    public override bool IsFinished()
    {
        return _touchedGround;
    }
}

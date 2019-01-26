using UnityEngine;
using UnityEngine.XR;

public class MenuState : GameState
{
    public FlapData _left;
    public FlapData _right;
    public float _flapValidTime = 1f;
    public GameObject _titleScreen;
    public GameObject _subTitle;
    public PlayerMovement _playerMovement;
    public PlayerPositionReset _playerPositionReset;
    public Rigidbody _playerRigidBody;
    public float _minWait = 2f;

    private bool _headSetPositioned;
    private bool _minWaitOver;
    private float _lastLeftFlap;
    private float _lastRightFlap;

    void Start()
    {
        _left.onFlap += SetLeftFlapped;
        _right.onFlap += SetRightFlapped;

        XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
    }

    protected void OnEnable()
    {
        _playerMovement.enabled = false;
        _titleScreen.SetActive(true);
        _subTitle.SetActive(false);
        _playerRigidBody.isKinematic = true;
        _playerMovement.applyDrift = false;
        _playerMovement.applyLift = false;

        _playerPositionReset.ResetPos();

        Invoke("MinWaitOver", _minWait);

        base.OnEnable();
    }

    void Update()
    {
        if (XRDevice.userPresence == UserPresenceState.Present && !_headSetPositioned && !_playerPositionReset.ResetSuccessfull())
        {
            _playerPositionReset.ResetPos();
            _headSetPositioned = true;
        }
    }

    void OnDisable()
    {
        _playerMovement.enabled = true;
        _titleScreen.SetActive(false);
        _minWaitOver = false;
        _playerRigidBody.isKinematic = false;
        _headSetPositioned = false;
    }

    private void MinWaitOver()
    {
        _minWaitOver = true;
        _subTitle.SetActive(true);
    }

    private void SetLeftFlapped()
    {
        _lastLeftFlap = Time.time;
    }

    private void SetRightFlapped()
    {
        _lastRightFlap = Time.time;
    }

    public override bool IsFinished()
    {
        var leftRecentFlap = Time.time - _lastLeftFlap < _flapValidTime;
        var rightRecentFlap = Time.time - _lastRightFlap < _flapValidTime;

        return leftRecentFlap && rightRecentFlap && XRDevice.isPresent && _minWaitOver;
    }
}

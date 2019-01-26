using UnityEngine;

public class PlayerPositionReset : MonoBehaviour
{
    public Transform _playArea;
    public Transform _head;
    public Transform _target;
    public Rigidbody _rigidbody;

    void Start()
    {
        ResetPos();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ResetPos();
    }

    public bool ResetSuccessfull()
    {
        return Mathf.Abs(_head.position.x - _target.position.x) < .2f &&
               Mathf.Abs(_head.position.z - _target.position.z) < .2f;
    }

    public void ResetPos()
    {
        _playArea.position = _target.position;
        _playArea.Translate(_playArea.position - _head.position);

        _rigidbody.velocity = Vector3.zero;
    }
}

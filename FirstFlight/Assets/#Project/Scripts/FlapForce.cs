using UnityEditorInternal;
using UnityEngine;

public class FlapForce : MonoBehaviour
{
    public float force;

    [Space(25)] 
    private Vector3 _pos;
    private Vector3 _prevPos;
    private float _deltaY;

    private const float _cutoff = -0.005f;

    void Start()
    {
        _prevPos = transform.position;
    }

    void Update()
    {
        _pos = transform.position;
        _deltaY = _pos.y - _prevPos.y;

        force = _deltaY < _cutoff ? Mathf.Abs(_deltaY) : 0;

        _prevPos = transform.position;
    }
}

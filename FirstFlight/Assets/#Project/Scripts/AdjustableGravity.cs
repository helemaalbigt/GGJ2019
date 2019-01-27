using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdjustableGravity : MonoBehaviour
{
    public float _gravMin;
    public float _gravMax;
    public float _optimalSpeed;

    [Header("Debug")] public float _grav;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _grav = GetGravityFactor();
        _rigidBody.AddForce(Physics.gravity * _grav);
    }

    private float GetGravityFactor()
    {
        return Utils.MapValue(_rigidBody.velocity.magnitude, 0, _optimalSpeed, _gravMax, _gravMin);
    }
}

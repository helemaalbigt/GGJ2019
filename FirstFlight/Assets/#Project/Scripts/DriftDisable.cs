using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class DriftDisable : MonoBehaviour
{
    public PlayerMovement movement;

    void Update()
    {
        movement.applyDrift = HandNotTracking(XRNode.LeftHand) && HandNotTracking(XRNode.RightHand);
    }

    private bool HandNotTracking(XRNode hand)
    {
        return InputTracking.GetLocalPosition(hand) == Vector3.zero;
    }
}

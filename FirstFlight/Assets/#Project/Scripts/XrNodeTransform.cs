using UnityEngine;
using UnityEngine.XR;

public class XrNodeTransform : MonoBehaviour
{
    public XRNode hand;

    private void Update()
    {
        transform.localPosition = InputTracking.GetLocalPosition(hand);
        transform.localRotation = InputTracking.GetLocalRotation(hand);
    }
}

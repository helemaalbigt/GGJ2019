using System;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public event Action onTouchedGround;

    public LayerMask ground;

    void OnCollisionEnter(Collision col)
    {
        if (ground == (ground | (1 << col.gameObject.layer)))
        {
            if (onTouchedGround != null)
                onTouchedGround();
        }
    }
}

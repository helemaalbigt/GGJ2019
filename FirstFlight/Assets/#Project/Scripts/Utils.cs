using UnityEngine;

public static class Utils
{
    public static float MapValue(float value, float minA, float maxA, float minB, float maxB)
    {
        float normal = Mathf.InverseLerp(minA, maxA, value);
        return Mathf.Lerp(minB, maxB, normal);
    }
}

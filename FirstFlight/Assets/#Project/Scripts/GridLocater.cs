using System;
using UnityEngine;

public class GridLocater : MonoBehaviour
{
    public event Action<Vector2> onCoordinatesChanged;

    public Vector2 _coordinates;
    public Transform _target;

    private Vector2 _newCoords;

    void Update()
    {
        _newCoords = PosToCoordinates(_target.position);

        if(_newCoords != _coordinates)
            FireNewCoordinates();

        _coordinates = _newCoords;
    }

    private Vector2 PosToCoordinates(Vector3 pos)
    {
        return new Vector2(
            (Mathf.RoundToInt(pos.x) + Math.Sign(pos.x) * (Globals.GridSize / 2)) / Globals.GridSize,
            (Mathf.RoundToInt(pos.z) + Math.Sign(pos.z) * (Globals.GridSize / 2)) / Globals.GridSize
        );
    }

    private void FireNewCoordinates()
    {
        if (onCoordinatesChanged != null)
            onCoordinatesChanged(_newCoords);
    }
}

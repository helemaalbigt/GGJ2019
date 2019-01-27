using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject _tilePrefab;
    public GameObject _lowPolyTile;
    public GridLocater _gridLocator;
    public int[] _tileMargin = new int[4]{2, 1, 1, 1};
    public int[] _lowPolyTileMargin = new int[4] { 3, 3, 3, 1 };

    private int _arraySize;
    private int _lowPolyArraySize;
    private Transform[] _tilesInsitances;
    private Vector2[] _occupiedTiles;

    private Transform[] _lowPolyTileInstances;
    private Vector2[] _lowPolyOccupiedTiles;

    void Awake()
    {
        InitArrays();

        _gridLocator.onCoordinatesChanged += UpdateTiles;
        CoordsToOccupiedTiles(Vector2.zero);

        SpawnTiles();
        PositionTiles();
    }

    void OnDrawGizmosSelected()
    {
        if (_occupiedTiles == null || _occupiedTiles.Length == 0)
            return;

        Gizmos.color = Color.blue;
        foreach (var coord in _occupiedTiles)
        {
            Gizmos.DrawWireCube(new Vector3((coord * Globals.GridSize).x, 0, (coord * Globals.GridSize).y), Vector3.one * Globals.GridSize);
        }

        Gizmos.color = Color.yellow;
        foreach (var coord in _lowPolyOccupiedTiles)
        {
            Gizmos.DrawWireCube(new Vector3((coord * Globals.GridSize).x, 0, (coord * Globals.GridSize).y), Vector3.one * Globals.GridSize);
        }
    }

    private void UpdateTiles(Vector2 coords)
    {
        CoordsToOccupiedTiles(coords);
        PositionTiles();
    }

    private void CoordsToOccupiedTiles(Vector2 coords)
    {
        int w = 0;

        for (int i = -_tileMargin[3]; i <= _tileMargin[1]; i++)
        {
            for (int j = -_tileMargin[2]; j <= _tileMargin[0]; j++)
            {
                _occupiedTiles[w] = new Vector2(coords.x + i, coords.y + j);
                w++;
            }
        }
    }

    private void InitArrays()
    {
        _arraySize = (_tileMargin[0] + _tileMargin[2] + 1) * (_tileMargin[1] + _tileMargin[3] + 1);
        _tilesInsitances = new Transform[_arraySize];
        _occupiedTiles = new Vector2[_arraySize];

        _lowPolyArraySize = ((_lowPolyTileMargin[0] + _lowPolyTileMargin[2] + 1) * (_lowPolyTileMargin[1] + _lowPolyTileMargin[3] + 1)) - _arraySize;
        _lowPolyTileInstances = new Transform[_lowPolyArraySize];
        _lowPolyOccupiedTiles = new Vector2[_lowPolyArraySize];
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < _arraySize; i++)
        {
            var go = Instantiate(_tilePrefab);
            _tilesInsitances[i] = go.transform;
        }
    }

    private void PositionTiles()
    {
        for (int i = 0; i < _arraySize; i++)
        {
            _tilesInsitances[i].position = new Vector3(
                _occupiedTiles[i].x * Globals.GridSize,
                0,
                _occupiedTiles[i].y * Globals.GridSize
                );
        }
    }
}

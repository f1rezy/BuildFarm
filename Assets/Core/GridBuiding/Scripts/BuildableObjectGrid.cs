using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectGrid : MonoBehaviour
{
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);

    private BuildableObject _buildingPrefab;
    private Dictionary<Vector2Int, BuildableObject> _grid;

    public Action OnBuilded;

    private void Awake()
    {
        _grid = new Dictionary<Vector2Int, BuildableObject>(_gridSize.x * _gridSize.y);
    }

    private void ResetCurrentBuildable()
    {
        _buildingPrefab.SetNormalColor();
        _buildingPrefab.OnPositionSetted -= ResetCurrentBuildable;
        _buildingPrefab = null;
    }

    public void SetToGrid(Vector3 position, Vector2 size)
    {
        int xPosition = Mathf.RoundToInt(position.x + transform.position.x);
        int yPosition = Mathf.RoundToInt(position.z + transform.position.z);

        for (int x = xPosition; x < xPosition + size.x; x++)
        {
            for (int y = yPosition; y < yPosition + size.y; y++)
            {
                _grid[new Vector2Int(x, y)] = _buildingPrefab;
            }
        }

        OnBuilded?.Invoke();
    }

    public BuildableObject CreateBuilding(BuildableObject buildingPrefab, Vector3 position)
    {
        if (_buildingPrefab != null)
        {
            Destroy(_buildingPrefab.gameObject);
        }

        _buildingPrefab = Instantiate(buildingPrefab, position, Quaternion.identity);
        _buildingPrefab.Init(this, _cameraFollower);
        _buildingPrefab.OnPositionSetted += ResetCurrentBuildable;

        int x = Mathf.RoundToInt(_buildingPrefab.transform.position.x);
        int y = Mathf.RoundToInt(_buildingPrefab.transform.position.z);

        _buildingPrefab.SetColor(CheckAvailability(x, y));

        return _buildingPrefab;
    }

    public bool CheckAvailability(int x, int y)
    {
        bool available = true;
        Vector3 offset = transform.position;

        if (x < offset.x || x > _gridSize.x + offset.x - _buildingPrefab.Size.x) available = false;
        if (y < offset.z || y > _gridSize.y + offset.z - _buildingPrefab.Size.y) available = false;

        if (available && IsPlaceTaken(x, y)) available = false;

        return available;
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _buildingPrefab.Size.x; x++)
        {
            for (int y = 0; y < _buildingPrefab.Size.y; y++)
            {
                var position = new Vector2Int(placeX + x, placeY + y);

                if (IsCellTaken(position))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsCellTaken(Vector2Int position)
    {
        return _grid.ContainsKey(position) && _grid[position] != null;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0f, y), new Vector3(1f, 0.1f, 1f));
            }
        }
    }
}

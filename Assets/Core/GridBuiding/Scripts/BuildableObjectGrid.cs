using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);

    public Dictionary<Vector2Int, BuildableObject> _grid;
    private BuildableObject _flyingBuilding;

    private void Awake()
    {
        _grid = new Dictionary<Vector2Int, BuildableObject>(_gridSize.x * _gridSize.y);
    }

    private void SetNull()
    {
        _flyingBuilding.SetNormalColor();
        _flyingBuilding.OnPositionSetted -= SetNull;
        _flyingBuilding = null;
    }

    public void CreateBuilding(BuildableObject buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }

        _flyingBuilding = Instantiate(buildingPrefab);
        _flyingBuilding.Init(this);
        _flyingBuilding.OnPositionSetted += SetNull;
    }

    public bool CheckAvailability(int x, int y)
    {
        bool available = true;

        if (x < 0 || x > _gridSize.x - _flyingBuilding._size.x) available = false;
        if (y < 0 || y > _gridSize.y - _flyingBuilding._size.y) available = false;

        if (available && IsPlaceTaken(x, y)) available = false;

        return available;
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding._size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding._size.y; y++)
            {
                var position = new Vector2Int(placeX + x, placeY + y);

                if (_grid.ContainsKey(position)
                    && _grid[position] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

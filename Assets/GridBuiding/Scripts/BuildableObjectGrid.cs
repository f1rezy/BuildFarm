using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);

    private BuildableObject[,] _grid;
    private BuildableObject _flyingBuilding;
    private Camera _cameraMain;

    private void Awake()
    {
        _grid = new BuildableObject[_gridSize.x, _gridSize.y];

        _cameraMain = Camera.main;
    }

    public void StartPlacingBuilding(BuildableObject buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }

        _flyingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (_flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < 0 || x > _gridSize.x - _flyingBuilding._size.x) available = false;
                if (y < 0 || y > _gridSize.y - _flyingBuilding._size.y) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                _flyingBuilding.transform.position = new Vector3(x, 0f, y);
                _flyingBuilding.SetColor(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding._size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding._size.y; y++)
            {
                if (_grid[placeX + x, placeY + y] != null) return true;
            }
        }

        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding._size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding._size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flyingBuilding;
            }
        }

        _flyingBuilding.SetNormalColor();
        _flyingBuilding = null;
    }
}

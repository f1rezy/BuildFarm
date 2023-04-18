using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObjectGrid : MonoBehaviour
{
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
    [SerializeField] private JsonSaveService _jsonSaver;

    [SerializeField] private Yandex _yandexService;
    [SerializeField] private Progress _progressService;

    [SerializeField] private BuildableObject _fieldPrefab;
    [SerializeField] private BuildableObject _hangarPrefab;
    [SerializeField] private BuildableObject _marketPrefab;

    private BuildableObject _buildingPrefab;
    private Dictionary<Vector2Int, BuildableObject> _grid;

    public Action OnBuilded;

    public Dictionary<Vector2Int, BuildableObject> Grid => _grid;

    private void Awake()
    {
        var buildingInfos = new BuildingInfo[]
        {
            //��� ������� ��������� ������� ��� ������ ������� ���� � ������ ����, ���� ����� ����� ������� � ������ �� ������
            new BuildingInfo(-1, 6, _fieldPrefab.Size.x, _fieldPrefab.Size.y, "Field"),
            new BuildingInfo(-9, 6, _hangarPrefab.Size.x, _hangarPrefab.Size.y, "Hangar"),
            new BuildingInfo(5, 5, _marketPrefab.Size.x, _marketPrefab.Size.y, "Market"),
        };

        var gridInfo = new GridInfo(buildingInfos);

        var serverGridInfo = _progressService.GridInfo;

        if (serverGridInfo != null)
            gridInfo = serverGridInfo;
        InitFrom(gridInfo);
    }

    private void OnEnable()
    {
        OnBuilded += SaveToServer;
    }

    private void OnDisable()
    {
        OnBuilded -= SaveToServer;
    }

    private void SaveToServer()
    {
        _progressService.GridInfo = new GridInfo(this);
        _progressService.Save();
    }

    private void InitFrom(GridInfo gridInfo)
    {
        _grid = new Dictionary<Vector2Int, BuildableObject>();

        var buildings = gridInfo.BuildingInfos;

        for (int i = 0; i < buildings.Length; i++)
        {
            SetBuildingToGrid(buildings[i]);
        }
    }

    private void SetBuildingToGrid(BuildingInfo building)
    {
        var position = new Vector3(building.X, 0, building.Y);

        switch (building.Type)
        {
            case "Field":
                CreateBuildingAndSet(_fieldPrefab, position);
                break;
            case "Hangar":
                CreateBuildingAndSet(_hangarPrefab, position);
                break;
            case "Market":
                CreateBuildingAndSet(_marketPrefab, position);
                break;
            default:
                throw new Exception();
        }
    }

    private void ResetCurrentBuildable()
    {
        _buildingPrefab.SetNormalColor();
        _buildingPrefab.OnPositionSetted -= ResetCurrentBuildable;
        _buildingPrefab = null;
    }

    public void SetToGrid(Vector3 position, Vector2 size)
    {
        int xPosition = Mathf.RoundToInt(position.x - transform.position.x);
        int yPosition = Mathf.RoundToInt(position.z - transform.position.z);

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

        _buildingPrefab.SetColor(CheckAvailability());

        return _buildingPrefab;
    }

    public void CreateBuildingAndSet(BuildableObject building, Vector3 position)
    {
        //_buildingPrefab = Instantiate(building);
        //_buildingPrefab.transform.position = position;

        //_buildingPrefab.Init(this, _cameraFollower);
        //_buildingPrefab.OnPositionSetted += ResetCurrentBuildable;
        //_buildingPrefab.SetColor(CheckAvailability());

        //building.Set();
        var buildingClone = CreateBuilding(building, position);
        buildingClone.Set();
    }

    public bool CheckAvailability()
    {
        int x = Mathf.RoundToInt((_buildingPrefab.transform.position - transform.position).x);
        int y = Mathf.RoundToInt((_buildingPrefab.transform.position - transform.position).z);

        bool available = true;
        Vector3 offset = Vector3.zero; //transform.position;

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

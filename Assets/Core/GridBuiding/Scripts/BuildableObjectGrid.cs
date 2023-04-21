using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableObjectGrid : MonoBehaviour
{
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
    [SerializeField] private JsonSaveService _jsonSaver;

    [SerializeField] private TextAsset _textAsset;
    [SerializeField] private Root _root;
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
            new BuildingInfo()
            {
                X = -1,
                Y = 6,
                XSize = _fieldPrefab.Size.x,
                YSize = _fieldPrefab.Size.y,
                Type = "Field"
            },
            new BuildingInfo()
            {
                X = -9,
                Y = 6,
                XSize = _hangarPrefab.Size.x,
                YSize = _hangarPrefab.Size.y,
                Type = "Hangar"
            },
            new BuildingInfo()
            {
                X = 5,
                Y = 5,
                XSize = _marketPrefab.Size.x,
                YSize = _marketPrefab.Size.y,
                Type = "Market"
            },
        };

        // var json = "{\"BuildingInfos\":[{\"X\":38,\"Y\":42,\"XSize\":5,\"YSize\":5,\"Type\":\"Field\"},{\"X\":30,\"Y\":42,\"XSize\":7,\"YSize\":5,\"Type\":\"Hangar\"},{\"X\":44,\"Y\":41,\"XSize\":7,\"YSize\":7,\"Type\":\"Market\"},{\"X\":37,\"Y\":37,\"XSize\":5,\"YSize\":5,\"Type\":\"Field\"}]}";
        // GridInfo gridInfo2 = JsonUtility.FromJson<GridInfo>(_textAsset.text);
        // buildingInfos = gridInfo2.BuildingInfos;

        var gridInfo = new GridInfoBuilder(buildingInfos).GridInfo;
        gridInfo = GridInfoSaver.LoadGridInfoToText(_textAsset.text);
        var serverGridInfo = _progressService.gridInfo;

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
        _progressService.gridInfo = new GridInfoBuilder(this).GridInfo;
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
                var buildedHangar = CreateBuildingAndSet(_hangarPrefab, position);
                var view = buildedHangar.GetComponentInChildren<HangarWindow>();
                view.Root = _root;
                break;
            case "Market":
                var buildedMarket = CreateBuildingAndSet(_marketPrefab, position);
                var button = buildedMarket.GetComponentInChildren<MarketButton>().Button;
                button.onClick.AddListener(_root.MarketView.GetComponent<SmoothShowHide>().Show);
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
        int xPosition = Mathf.RoundToInt(position.x);
        int yPosition = Mathf.RoundToInt(position.z);

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

        if (_buildingPrefab.TryGetComponent(out Hangar hangar))
        {
            var view = hangar.GetComponentInChildren<HangarWindow>();
            view.Root = _root;
        }

        _buildingPrefab.SetColor(CheckAvailability());

        return _buildingPrefab;
    }

    public BuildableObject CreateBuildingAndSet(BuildableObject building, Vector3 position)
    {
        if (!PlayerPrefs.HasKey("SaveFile"))
            position += transform.position;

        var buildingClone = CreateBuilding(building, position);
        buildingClone.Set();
        return buildingClone;
    }

    public bool CheckAvailability()
    {
        int x = Mathf.RoundToInt((_buildingPrefab.transform.position).x);
        int y = Mathf.RoundToInt((_buildingPrefab.transform.position).z);

        bool available = true;
        Vector3 offset = transform.position; //transform.position;

        if (x < offset.x || x > _gridSize.x - offset.x - _buildingPrefab.Size.x) available = false;
        if (y < offset.z || y > _gridSize.y - offset.z - _buildingPrefab.Size.y) available = false;

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

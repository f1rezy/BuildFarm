using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableObject : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = Vector2Int.one;

    [SerializeField] private TopBuildingPopUp _buildPopUp;
    [SerializeField] private Button _buildButton;

    private CameraFollower _cameraFollower;
    private BuildableObjectRenderer _renderer;
    private BuildingTouchHandler _touchHandler;
    private BuildableObjectGrid _grid;

    private bool _available = true;

    public Vector2Int Size => _size;

    public Action<bool> OnPositionGhanged;
    public Action OnPositionSetted;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<BuildableObjectRenderer>();
        _touchHandler = gameObject.GetComponent<BuildingTouchHandler>();
    }

    public void Init(BuildableObjectGrid grid, CameraFollower cameraFollower)
    {
        _grid = grid;
        _cameraFollower = cameraFollower;
        OnPositionGhanged?.Invoke(_grid.CheckAvailability());
    }

    private void OnEnable()
    {
        OnPositionGhanged += SetColor;
        OnPositionGhanged += SetButtonInteractable;
        _touchHandler.OnDrag += Move;

        _buildButton.onClick.AddListener(Set);
    }

    private void OnDisable()
    {
        OnPositionGhanged -= SetColor;
        OnPositionGhanged += SetButtonInteractable;
        _touchHandler.OnDrag -= Move;
    }

    private void SetButtonInteractable(bool interactable) => _buildButton.interactable = interactable;

    private void Move(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x) - _size.x / 2;
        int y = Mathf.RoundToInt(position.z) - _size.y / 2;

        _available = _grid.CheckAvailability();
        transform.position = new Vector3(x, 0f, y);
        OnPositionGhanged?.Invoke(_available);
    }

    public void Set()
    {
        if (_available)
        {
            _grid.SetToGrid(transform.position, _size);
            _touchHandler.OnDrag -= Move;
            OnPositionSetted?.Invoke();
            _buildPopUp.Hide();
            _cameraFollower.SetCharacterTarget();
        }
    }

    public void SetColor(bool canBuild)
    {
        _renderer.SetColorOnPositionChanged(canBuild);
    }

    public void SetNormalColor()
    {
        _renderer.SetDefault();
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0f, y), new Vector3(1f, .1f, 1f));
            }
        }
    }
}

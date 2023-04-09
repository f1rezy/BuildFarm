using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    [SerializeField] public Vector2Int _size = Vector2Int.one;

    private BuildableObjectRenderer _renderer;
    private BuildingTouchHandler _touchHandler;
    private BuildableObjectGrid _grid;

    private bool _available = false;

    public Action<bool> OnPositionGhanged;
    public Action OnPositionSetted;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<BuildableObjectRenderer>();
        _touchHandler = gameObject.GetComponent<BuildingTouchHandler>();
    }

    public void Init(BuildableObjectGrid grid)
    {
        _grid = grid;
    }

    private void OnEnable()
    {
        OnPositionGhanged += SetColor;
        _touchHandler.OnDrag += Move;
        _touchHandler.OnExit += Set;
    }

    private void OnDisable()
    {
        OnPositionGhanged -= SetColor;
        _touchHandler.OnDrag -= Move;
        _touchHandler.OnExit -= Set;
    }

    private void Move(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x) - _size.x;
        int y = Mathf.RoundToInt(position.z) - _size.y;

        _available = _grid.CheckAvailability(x, y);
        transform.position = new Vector3(x, 0f, y);
        OnPositionGhanged?.Invoke(_available);
    }

    private void Set()
    {
        if (_available)
        {
            _touchHandler.OnDrag -= Move;
            OnPositionSetted?.Invoke();
        }
    }

    public void SetColor(bool canBuild)
    {
        if (canBuild is true)
        {
            _renderer.OnCanBuild();
        }
        else
        {
            _renderer.OnCantBuild();
        }
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    [SerializeField] public Vector2Int _size = Vector2Int.one;

    private BuildableObjectRenderer _renderer;

    public Action<bool> OnPositionGhanged;

    private void Start()
    {
        _renderer = gameObject.GetComponent<BuildableObjectRenderer>();
    }

    private void OnEnable()
    {
        OnPositionGhanged += SetColor;
    }

    private void OnDisable()
    {
        OnPositionGhanged -= SetColor;
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
}

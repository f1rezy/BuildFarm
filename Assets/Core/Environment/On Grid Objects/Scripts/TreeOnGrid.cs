using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOnGrid : MonoBehaviour
{
    [SerializeField] private BuildableObjectGrid _grid;

    private Vector2Int _position;

    private void Awake()
    {
        _position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

    private void OnEnable()
    {
        _grid.OnBuilded += CheckSeldPosition;
    }

    private void OnDisable()
    {
        _grid.OnBuilded -= CheckSeldPosition;
    }

    private void CheckSeldPosition()
    {
        if (!_grid.CheckAvailability(_position.x, _position.y))
            Destroy();

    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}

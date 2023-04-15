using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEnviromentalObject : MonoBehaviour
{
    [SerializeField] private BuildableObjectGrid _grid;

    private Vector2Int _position;

    private void Start()
    {
        _position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

    private void OnEnable()
    {
        _grid.OnBuilded += CheckSelfPosition;
    }

    private void OnDisable()
    {
        _grid.OnBuilded -= CheckSelfPosition;
    }

    private void CheckSelfPosition()
    {
        var gridPosition = new Vector2Int(Mathf.RoundToInt(_grid.transform.position.x), Mathf.RoundToInt(_grid.transform.position.z));

        if (_grid.IsCellTaken(gridPosition + _position))
            Destroy();
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}

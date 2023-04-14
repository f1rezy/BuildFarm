using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour, IProductiveBuilding, IPopUpSupporter
{
    [SerializeField] private Vector2Int _fieldSize;
    [SerializeField] private Vector3 _offset = new Vector3(1f, 0f, 1f);
    [SerializeField] private FieldMineableItem _fieldMineableItemPrefab;

    private FieldMineableItem[,] _field;
    private float _fieldGrowthProgess = 0f;

    public event Action OnGrowthProgressChanged;

    private void Start()
    {
        _field = new FieldMineableItem[_fieldSize.x, _fieldSize.y];

        InitField();
    }

    private void InitField()
    {
        var itemsStartPosition = transform.position + _offset;

        for (int i = 0; i < _fieldSize.x; i++)
        {
            for (int j = 0; j < _fieldSize.y; j++)
            {
                var itemPosition = itemsStartPosition + new Vector3(i, 0, j);
                _field[i, j] = Instantiate(_fieldMineableItemPrefab, itemPosition, Quaternion.identity, this.transform);
                _field[i, j].OnGrowthProgressChanged += (t) => RecalculateFieldGrowthProgess();
            }
        }
    }

    public string GetPopUpText()
    {
        return $"{Mathf.Round(_fieldGrowthProgess * 100)}%";
    }

    private void RecalculateFieldGrowthProgess()
    {
        var progress = 0f;

        foreach (var item in _field)
            progress += item.GrowthProgress;

        _fieldGrowthProgess = progress / _field.Length;
        OnGrowthProgressChanged?.Invoke();
    }
}

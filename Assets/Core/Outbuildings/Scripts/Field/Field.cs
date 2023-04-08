using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour, IProductiveBuilding
{
    [SerializeField] private Vector2Int _fieldSize;
    [SerializeField] private FieldMineableItem _fieldMineableItemPrefab;

    private FieldMineableItem[,] _field;

    private void Start()
    {
        _field = new FieldMineableItem[_fieldSize.x, _fieldSize.y];

        InitField();
    }

    private void InitField()
    {
        var itemsStartPosition = transform.position + new Vector3(1.5f, 0, 1.5f);

        for (int i = 0; i < _fieldSize.x; i++)
        {
            for (int j = 0; j < _fieldSize.y; j++)
            {
                var itemPosition = itemsStartPosition + new Vector3(i, 0, j);
                _field[i, j] = Object.Instantiate(_fieldMineableItemPrefab, itemPosition, Quaternion.identity, this.transform);
            }
        }
    }
}

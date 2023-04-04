using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _defaultPosition;

    private List<T> _pool;
    private bool _autoExpand;
    private bool _activeByDefault;

    public PoolMono(T prefab, Transform defaultPosition, int count, bool autoExpand, bool activeByDefault)
    {
        _prefab = prefab;
        _autoExpand = autoExpand;
        _activeByDefault = activeByDefault;
        _defaultPosition = defaultPosition;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateElement();
    }

    private T CreateElement()
    {
        var created = Object.Instantiate(_prefab, _defaultPosition);
        created.gameObject.SetActive(_activeByDefault);
        _pool.Add(created);
        return created;
    }

    public T GetFreeElement(Transform position)
    {
        if (!HasFreeElement(out T element))
        {
            if (_autoExpand)
                element = CreateElement();
            else
                throw new System.Exception($"no free elements in pool of {typeof(T)}");
        }

        element.gameObject.SetActive(true);
        element.transform.position = position.position;
        return element;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                return true;
            }
        }
        element = null;
        return false;
    }
}

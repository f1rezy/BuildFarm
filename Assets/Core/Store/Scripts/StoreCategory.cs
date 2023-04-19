using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class StoreCategory : MonoBehaviour
{
    [SerializeField] private GameObject _categoryView;
    [SerializeField] private GameObject _itemsRowPrefab;
    [SerializeField] private StoreItem[] _items;

    private void Mount(GameObject itemsRow, StoreItem item)
    {
        if (item.gameObject != null)
        {
            if (item.isActiveAndEnabled && item.gameObject.activeInHierarchy)
            {
                item.gameObject.transform.SetParent(itemsRow.gameObject.transform);
            }
            else
            {
                Instantiate(item, itemsRow.transform);
                Destroy(item.gameObject);
            }
        }
    }

    private void Init()
    {
        GameObject itemsRow = Instantiate(_itemsRowPrefab, _categoryView.GetComponent<ScrollRect>().content);
        for (var i = 0; i < _items.Length; i++)
        {
            if (i != 0 && i % 3 == 0) itemsRow = Instantiate(_itemsRowPrefab, _categoryView.GetComponent<ScrollRect>().content);
            Mount(itemsRow, _items[i]);
        }
    }

    public void Start()
    {
        Init();
    }

    public void Show()
    {
        _categoryView.SetActive(true);
    }

    public void Hide()
    {
        _categoryView.SetActive(false);
    }
}
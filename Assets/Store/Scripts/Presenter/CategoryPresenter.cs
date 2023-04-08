using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CategoryPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _categoryViewPrefab;
    [SerializeField] private GameObject _itemsRowPrefab;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private ItemPresenter _itemPrefab;
    
    public GameObject background;
    public Category category;
    private GameObject _categoryView;

    private void Mount(GameObject itemsRow, Item item)
    {
        ItemPresenter itemPresenter = Instantiate(_itemPrefab, itemsRow.transform);
        itemPresenter.item = item;
    }
    
    private void Init()
    {
        _categoryView = Instantiate(_categoryViewPrefab, background.transform);
        GameObject itemsRow = Instantiate(_itemsRowPrefab, _categoryView.GetComponent<ScrollRect>().content);
        for (var i = 0; i < category.Items.Count; i++)
        {
            if(i != 0 && i % 3 == 0) itemsRow = Instantiate(_itemsRowPrefab, _categoryView.GetComponent<ScrollRect>().content);
            Mount(itemsRow, category.Items[i]);
        }
    }
    
    public void Start()
    {
        _title.text = category.CategoryName;
        Init();
    }

    public void Show()
    {
        _categoryView.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _categoryView.gameObject.SetActive(false);
    }
}
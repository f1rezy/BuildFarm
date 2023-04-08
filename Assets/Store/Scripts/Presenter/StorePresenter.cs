using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePresenter : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _categoriesPlace;
    [SerializeField] private CategoryPresenter _categoryPrefab;
    
    private Store _store;
    private List<CategoryPresenter> _categoryPresenters;

    private void SetActiveCategory(int idx)
    {
        foreach (var category in _categoryPresenters)
        {
            category.Hide();
        }
        _categoryPresenters[idx].Show();
    }
    private void Mount(Category category)
    {
        CategoryPresenter categoryPresenter = Instantiate(_categoryPrefab, _categoriesPlace.transform);
        int idx = _categoryPresenters.Count;
        categoryPresenter.GetComponent<Button>().onClick.AddListener(() => SetActiveCategory(idx));
        categoryPresenter.category = category;
        categoryPresenter.background = _background;
        _categoryPresenters.Add(categoryPresenter);
    }
    
    private void Init()
    {
        foreach (var category in _store.Categories)
        {
            Mount(category);
        }
    }
    
    private void Awake()
    {
        _categoryPresenters = new List<CategoryPresenter>();
        _store = new Store(new List<Category> {
            new Category("Инструменты", new List<Item>
            {
                new Item("Инструмент", "", "Это инструмент", 100)
            }),
            new Category("Постройки", new List<Item>
            {
                new Item("Постройка", "", "Это постройка", 100)
            }),
            new Category("Техника", new List<Item>
            {
                new Item("Техника", "", "Это техника", 100)
            })
        });
        Init();
        SetActiveCategory(2);
    }
}

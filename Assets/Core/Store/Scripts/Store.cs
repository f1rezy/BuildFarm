using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private StoreCategory[] _categories;

    private void SetActiveCategory(int idx)
    {
        for (int i = 0; i < _categories.Length; i++)
        {
            if (i == idx)
            {
                _categories[i].Show();
            }
            else
            {
                _categories[i].Hide();
            }
        }
        
    }
    private void Mount(int idx, StoreCategory category)
    {
        category.GetComponent<Button>().onClick.AddListener(() => SetActiveCategory(idx));
    }
    
    private void Init()
    {
        for (var i = 0; i < _categories.Length; i++)
        {
            Mount(i, _categories[i]);
        }
    }
    
    private void Awake()
    {
        Init();
        SetActiveCategory(0);
    }
}

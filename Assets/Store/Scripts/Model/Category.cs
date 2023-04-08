using System.Collections.Generic;
public class Category
{
    public string CategoryName { get; }
    public List<Item> Items { get; }

    public Category(string name, List<Item> items)
    {
        CategoryName = name;
        Items = items;
    }
}

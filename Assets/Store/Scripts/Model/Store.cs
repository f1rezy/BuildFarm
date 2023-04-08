using System.Collections.Generic;
public class Store
{
    public List<Category> Categories { get; }

    public Store(List<Category> categories)
    {
        Categories = categories;
    }
}

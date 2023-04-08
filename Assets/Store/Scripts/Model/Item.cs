public class Item
{
    public string Title { get; }

    public string ImagePath { get; }

    public string Description { get; }

    public int Cost { get; }

    public IPurchasableItem purchasableItem;

    public Item(string title,
        string imagePath,
        string description,
        int cost)
    {
        Title = title;
        ImagePath = imagePath;
        Description = description;
        Cost = cost;
    }
}
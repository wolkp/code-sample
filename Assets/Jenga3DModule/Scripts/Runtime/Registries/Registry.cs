using System.Collections.Generic;

public abstract class Registry<T>
{
    private List<T> _items = new List<T>();

    public void AddItem(T item)
    {
        _items.Add(item);
    }

    public bool RemoveItem(T item)
    {
        return _items.Remove(item);
    }

    public IEnumerable<T> GetAllItems()
    {
        return _items.AsReadOnly();
    }
}
using System.Collections.Generic;
using System.Linq;
using dotnet_catalog_api.Models;

namespace dotnet_catalog_api.Repositories;

public class InMemItemsRepository : IItemsRepository 
{
    private readonly List<Item> _items = new()
    {
        new Item { Id = Guid.NewGuid(), Name = "Potions", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
        new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
    };

    public IEnumerable<Item> GetItems()
    {
        return _items;
    }

    public Item GetItem(Guid id)
    {
        return _items.Where(x => x.Id == id).SingleOrDefault();
    }

    public void CreateItem(Item item)
    {
        _items.Add(item);
    }

    public void UpdateItem(Item item)
    {
        var index = _items.FindIndex(existingItem => existingItem.Id == item.Id);
        _items[index] = item;
    }

    public void DeleteItem(Guid id)
    {
        var index = _items.FindIndex(existingItem => existingItem.Id == id);
        _items.RemoveAt(index);
    }
}

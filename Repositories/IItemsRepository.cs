using System.Collections.Generic;
using dotnet_catalog_api.Models;

namespace dotnet_catalog_api.Repositories;

public interface IItemsRepository
{
    Item GetItem(Guid id);
    IEnumerable<Item> GetItems();
    void CreateItem(Item item);
    void UpdateItem(Item item);
    void DeleteItem(Guid id);
}

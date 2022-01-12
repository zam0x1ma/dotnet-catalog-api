using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_catalog_api.Models;

namespace dotnet_catalog_api.Repositories;

public interface IItemsRepository
{
    Task<Item> GetItemAsync(Guid id);
    Task<IEnumerable<Item>> GetItemsAsync();
    Task CreateItemAsync(Item item);
    Task UpdateItemAsync(Item item);
    Task DeleteItemAsync(Guid id);
}

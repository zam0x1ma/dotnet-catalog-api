using dotnet_catalog_api.DTOs;
using dotnet_catalog_api.Models;

namespace dotnet_catalog_api;

public static class Extensions
{
    public static ItemDTO AsDTO(this Item item)
    {
        return new ItemDTO
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            CreatedDate = item.CreatedDate
        };
    }
}

using System.ComponentModel.DataAnnotations;

namespace dotnet_catalog_api.DTOs;

public record CreateItemDTO
{
    [Required]
    public string Name { get; init; }
    [Required]
    [Range(1, 1000)]
    public decimal Price { get; init; }
}

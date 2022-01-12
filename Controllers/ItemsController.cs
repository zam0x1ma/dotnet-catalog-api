using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using dotnet_catalog_api.DTOs;
using dotnet_catalog_api.Models;
using dotnet_catalog_api.Repositories;

namespace dotnet_catalog_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository _repository;

    public ItemsController(IItemsRepository repository)
    {
        _repository = repository;
    }

    // GET /items
    [HttpGet]
    public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
    {
        var items = (await _repository.GetItemsAsync())
                    .Select(item => item.AsDTO());
        return items;
    }

    // GET /items/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid id)
    {
        var item = await _repository.GetItemAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return item.AsDTO();
    }

    // POST /items
    [HttpPost]
    public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO itemDTO)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemDTO.Name,
            Price = itemDTO.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _repository.CreateItemAsync(item);

        return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDTO());
    }

    // PUT /items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDTO itemDTO)
    {
        var existingItem = await _repository.GetItemAsync(id);

        if (existingItem is null)
        {
            return NotFound();
        }

        Item updatedItem = existingItem with
        {
            Name = itemDTO.Name,
            Price = itemDTO.Price
        };

        await _repository.UpdateItemAsync(updatedItem);

        return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
        var existingItem = await _repository.GetItemAsync(id);

        if (existingItem is null)
        {
            return NotFound();
        }

        await _repository.DeleteItemAsync(id);

        return NoContent();

    }
}

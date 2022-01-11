using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
    public IEnumerable<ItemDTO> GetItems()
    {
        return _repository.GetItems().Select(item => item.AsDTO());
    }

    // GET /items/{id}
    [HttpGet("{id}")]
    public ActionResult<ItemDTO> GetItem(Guid id)
    {
        var item = _repository.GetItem(id);

        if (item is null)
        {
            return NotFound();
        }

        return item.AsDTO();
    }

    // POST /items
    [HttpPost]
    public ActionResult<ItemDTO> CreateItem(CreateItemDTO itemDTO)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Name = itemDTO.Name,
            Price = itemDTO.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        _repository.CreateItem(item);

        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDTO());
    }

    // PUT /items/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDTO itemDTO)
    {
        var existingItem = _repository.GetItem(id);

        if (existingItem is null)
        {
            return NotFound();
        }

        Item updatedItem = existingItem with
        {
            Name = itemDTO.Name,
            Price = itemDTO.Price
        };

        _repository.UpdateItem(updatedItem);

        return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
        var existingItem = _repository.GetItem(id);

        if (existingItem is null)
        {
            return NotFound();
        }

        _repository.DeleteItem(id);

        return NoContent();

    }
}

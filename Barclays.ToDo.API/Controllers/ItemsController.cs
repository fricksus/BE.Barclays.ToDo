using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barclays.ToDo.Services.Contracts;
using Barclays.ToDo.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Barclays.ToDo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _itemService.GetAllAsync();
           
            return items.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var student = await _itemService.GetAsync(id);
           
            if (student is null) return NotFound();
            
            return student;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(int id, ItemDto itemDto)
        {
            if (id != itemDto.Id) return BadRequest();
            
            var isUpdated = await _itemService.UpdateAsync(itemDto);
            
            if (!isUpdated) return BadRequest();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(ItemDto itemDto)
        {
            itemDto = await _itemService.CreateAsync(itemDto);
            
            if (itemDto is null) return BadRequest();

            return CreatedAtAction("GetItem", new {id = itemDto.Id}, itemDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDto>> DeleteItem(int id)
        {
            var item = await _itemService.DeleteAsync(id);

            if (item is null) return NotFound();

            return item;
        }
    }
}
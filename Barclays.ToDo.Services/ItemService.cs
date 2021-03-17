using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Barclays.ToDo.Data.Contracts;
using Barclays.ToDo.Data.Models;
using Barclays.ToDo.Services.Contracts;
using Barclays.ToDo.Services.DTOs;

namespace Barclays.ToDo.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<ItemDto> GetAsync(int id)
        {
            var item = await _itemRepository.GetAsync(id);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<List<ItemDto>> GetAllAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return _mapper.Map<List<ItemDto>>(items);
        }

        public async Task<ItemDto> CreateAsync(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);

            if(!await ValidItem(item)) return null;

            item = await _itemRepository.Create(item);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<bool> UpdateAsync(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);

            if (!await ValidItem(item)) return false;

            _itemRepository.Update(item);
            return true;
        }

        public async Task<ItemDto> DeleteAsync(int id)
        {
            var item = await _itemRepository.GetAsync(id);

            if (item is null) return null;

            _itemRepository.Delete(item);
            return _mapper.Map<ItemDto>(item);
        }

        private async Task<bool> ValidItem(Item item)
        {
            var sameNameRecord = await _itemRepository.FindAsync(record => record.Name == item.Name);
            if (sameNameRecord.Any()) return false;
            return true;
        }
    }
}
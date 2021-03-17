using System.Collections.Generic;
using System.Threading.Tasks;
using Barclays.ToDo.Services.DTOs;

namespace Barclays.ToDo.Services.Contracts
{
    public interface IItemService
    {
        Task<ItemDto> GetAsync(int id);
        Task<List<ItemDto>> GetAllAsync();
        Task<ItemDto> CreateAsync(ItemDto item);
        Task<bool> UpdateAsync(ItemDto item);
        Task<ItemDto> DeleteAsync(int id);
    }
}
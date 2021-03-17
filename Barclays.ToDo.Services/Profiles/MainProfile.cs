using AutoMapper;
using Barclays.ToDo.Data.Models;
using Barclays.ToDo.Services.DTOs;

namespace Barclays.ToDo.Services.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Barclays.ToDo.Data.Contracts;
using Barclays.ToDo.Data.Enums;
using Barclays.ToDo.Data.Models;
using Barclays.ToDo.Services;
using Barclays.ToDo.Services.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barclays.ToDo.Tests.Services
{
    [TestClass]
    public class ItemServiceTests
    {
        [TestMethod]
        public async Task GetAsyncTest()
        {
            const int id = 0;
            var item = GetItem();
            var itemDto = GetItemDto();
            var mockRepository = new Mock<IItemRepository>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(item);
            mockMapper.Setup(mapper => mapper.Map<ItemDto>(item))
                .Returns(itemDto);
            var service = new ItemService(mockMapper.Object, mockRepository.Object);

            var result = await service.GetAsync(id);

            Assert.AreEqual(itemDto, result);
        }

        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            var item = GetListOfItems();
            var itemDto = GetListOfDtos();
            var mockRepository = new Mock<IItemRepository>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(item);
            mockMapper.Setup(mapper => mapper.Map<List<ItemDto>>(item))
                .Returns(itemDto);
            var service = new ItemService(mockMapper.Object, mockRepository.Object);

            var result = await service.GetAllAsync();

            Assert.AreEqual(itemDto, result);
        }

        [TestMethod]
        public async Task CreateAsyncTest()
        {
            var item = GetItem();
            var itemDto = GetItemDto();
            var mockRepository = new Mock<IItemRepository>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repository => repository.Create(item))
                .ReturnsAsync(item);
            mockMapper.Setup(mapper => mapper.Map<Item>(itemDto))
                .Returns(item);
            mockMapper.Setup(mapper => mapper.Map<ItemDto>(item))
                .Returns(itemDto);
            var service = new ItemService(mockMapper.Object, mockRepository.Object);

            var result = await service.CreateAsync(itemDto);

            Assert.AreEqual(itemDto, result);
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            var item = GetItem();
            var itemDto = GetItemDto();
            var mockRepository = new Mock<IItemRepository>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repository => repository.Update(item));
            mockMapper.Setup(mapper => mapper.Map<Item>(itemDto))
                .Returns(item);
            var service = new ItemService(mockMapper.Object, mockRepository.Object);

            var result = await service.UpdateAsync(itemDto);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            const int id = 0;
            var item = GetItem();
            var itemDto = GetItemDto();
            var mockRepository = new Mock<IItemRepository>();
            var mockMapper = new Mock<IMapper>();
            mockRepository.Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(item);
            mockRepository.Setup(repository => repository.Delete(item));
            mockMapper.Setup(mapper => mapper.Map<ItemDto>(item))
                .Returns(itemDto);
            var service = new ItemService(mockMapper.Object, mockRepository.Object);

            var result = await service.DeleteAsync(id);

            Assert.AreEqual(itemDto, result);
        }

        private List<Item> GetListOfItems()
        {
            var list = new List<Item>
            {
                new Item
                {
                    Id = 0,
                    Priority = 0,
                    Name = "First Task",
                    Status = Status.NotStarted
                },

                new Item
                {
                    Id = 1,
                    Priority = 1,
                    Name = "Second Task",
                    Status = Status.NotStarted
                }
            };

            return list;
        }

        private Item GetItem()
        {
            return new Item
            {
                Id = 0,
                Priority = 0,
                Name = "FirstTask",
                Status = Status.NotStarted
            };
        }

        private ItemDto GetItemDto()
        {
            return new ItemDto
            {
                Id = 0,
                Priority = 0,
                Name = "FirstTask",
                Status = ToDo.Services.Enums.Status.NotStarted
            };
        }

        private List<ItemDto> GetListOfDtos()
        {
            var list = new List<ItemDto>
            {
                new ItemDto
                {
                    Id = 0,
                    Priority = 0,
                    Name = "First Task",
                    Status = ToDo.Services.Enums.Status.NotStarted
                },

                new ItemDto
                {
                    Id = 1,
                    Priority = 1,
                    Name = "Second Task",
                    Status = ToDo.Services.Enums.Status.NotStarted
                }
            };

            return list;
        }
    }
}
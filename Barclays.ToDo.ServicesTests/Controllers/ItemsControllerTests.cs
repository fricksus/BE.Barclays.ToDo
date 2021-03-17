using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barclays.ToDo.API.Controllers;
using Barclays.ToDo.Services.Contracts;
using Barclays.ToDo.Services.DTOs;
using Barclays.ToDo.Services.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Barclays.ToDo.Tests.Controllers
{
    [TestClass]
    public class ItemsControllerTests
    {
        [TestMethod]
        public async Task GetItemsTest()
        {
            var mockService = new Mock<IItemService>();
            mockService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(GetListOfItemDtos());
            var controller = new ItemsController(mockService.Object);

            var result = await controller.GetItems();

            Assert.AreEqual(2, result.Value.Count());
        }

        [TestMethod]
        public async Task GetItemTest()
        {
            var mockService = new Mock<IItemService>();
            const int id = 0;
            var itemDto = GetItemDto();
            mockService.Setup(service => service.GetAsync(id))
                .ReturnsAsync(itemDto);
            var controller = new ItemsController(mockService.Object);

            var result = await controller.GetItem(id);

            Assert.IsNotInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(result.Value, itemDto);
        }

        [TestMethod]
        public async Task GetItemTest_NotFound()
        {
            var mockService = new Mock<IItemService>();
            const int id = 1;
            mockService.Setup(service => service.GetAsync(id))
                .ReturnsAsync(null as ItemDto);
            var controller = new ItemsController(mockService.Object);

            var result = await controller.GetItem(id);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutItemTest()
        {
            var mockService = new Mock<IItemService>();
            const int id = 0;
            var itemDto = GetItemDto();
            mockService.Setup(service => service.UpdateAsync(itemDto))
                .ReturnsAsync(true);
            var controller = new ItemsController(mockService.Object);

            var result = await controller.PutItem(id, itemDto);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutItemTest_BadRequest()
        {
            var mockService = new Mock<IItemService>();
            const int id = 1;
            var itemDto = GetItemDto();
            mockService.Setup(service => service.UpdateAsync(itemDto))
                .ReturnsAsync(true);
            var controller = new ItemsController(mockService.Object);

            var result = await controller.PutItem(id, itemDto);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostItemTest_BadRequest()
        {
            var mockService = new Mock<IItemService>();
            var itemDto = GetItemDto();
            mockService.Setup(service => service.CreateAsync(itemDto))
                .ReturnsAsync(null as ItemDto);
            var controller = new ItemsController(mockService.Object);

            var result = await controller.PostItem(itemDto);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostItemTest()
        {
            var mockService = new Mock<IItemService>();
            var itemDto = GetItemDto();
            mockService.Setup(service => service.CreateAsync(itemDto))
                .ReturnsAsync(itemDto);
            var controller = new ItemsController(mockService.Object);

            var result = await controller.PostItem(itemDto);

            Assert.IsNotInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteItemTest_NotFound()
        {
            var mockService = new Mock<IItemService>();
            var itemDto = GetItemDto();
            const int id = 1;
            mockService.Setup(service => service.DeleteAsync(id))
                .ReturnsAsync(null as ItemDto);
            var controller = new ItemsController(mockService.Object);

            var result = await controller.DeleteItem(id);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        private ItemDto GetItemDto()
        {
            return new ItemDto
            {
                Id = 0,
                Priority = 0,
                Name = "FirstTask",
                Status = Status.NotStarted
            };
        }

        private List<ItemDto> GetListOfItemDtos()
        {
            var list = new List<ItemDto>
            {
                new ItemDto
                {
                    Id = 0,
                    Priority = 0,
                    Name = "First Task",
                    Status = Status.NotStarted
                },

                new ItemDto
                {
                    Id = 1,
                    Priority = 1,
                    Name = "Second Task",
                    Status = Status.NotStarted
                }
            };

            return list;
        }
    }
}
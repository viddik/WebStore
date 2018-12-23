using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using WebStore.Controllers;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.ViewModel.Cart;
using WebStore.Domain.ViewModel.Order;
using WebStore.Domain.ViewModel.Product;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            var mockCartService = new Mock<ICartService>();
            var mockOrdersService = new Mock<IOrdersService>();
            var controller = new CartController(mockCartService.Object, mockOrdersService.Object);
            controller.ModelState.AddModelError("error", "InvalidModel");

            var result = controller.CheckOut(
                new OrderViewModel()
                {
                    Name = "test"
                });

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<DetailsViewModel>(viewResult.ViewData.Model);
            Assert.Equal("test", model.OrderViewModel.Name);
        }

        [TestMethod]
        public void CheckOut_Calls_Service_And_Return_Redirect()
        {
            #region Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                }));

            // setting up cartService
            var mockCartService = new Mock<ICartService>();
            mockCartService.Setup(c => c.TransformCart()).Returns(
                new CartViewModel()
                {
                    Items = new Dictionary<ProductItemViewModel, int>()
                    {
                        { new ProductItemViewModel(), 1 }
                    }
                });

            // setting up ordersService
            var mockOrdersService = new Mock<IOrdersService>();
            mockOrdersService.Setup(c => c.CreateOrder(It.IsAny<CreateOrderModel>(), It.IsAny<string>()))
                .Returns(new OrderDto() { Id = 1 });

            var controller = new CartController(mockCartService.Object, mockOrdersService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = user
                    }
                }
            };
            #endregion

            // Act
            var result = controller.CheckOut(new OrderViewModel()
            {
                Name = "test",
                Address = "",
                Phone = ""
            });

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal("OrderConfirmed", redirectResult.ActionName);
            Assert.Equal(1, redirectResult.RouteValues["id"]);
        }
    }
}

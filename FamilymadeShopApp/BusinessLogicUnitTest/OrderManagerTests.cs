using BusinessLogicLayer.Managers;
using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using Moq;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicUnitTest
{
    [TestClass]
    public class OrderManagerTests
    {
        private Mock<IOrderRepository> _mockOrderRepository;
        private OrderManager _orderManager;

        [TestInitialize]
        public void Setup()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _orderManager = new OrderManager(_mockOrderRepository.Object);
        }

        [TestMethod]
        public void AddOrder_ShouldCallAddOrderDAL()
        {
            // Arrange
            var order = new Order();

            // Act
            _orderManager.AddOrder(order);

            // Assert
            _mockOrderRepository.Verify(x => x.AddOrderDAL(order), Times.Once);
        }

        [TestMethod]
        public void GetOrdersByUserId_ShouldCallGetOrdersByUserIdDAL()
        {
            // Arrange
            int userId = 1;

            // Act
            _orderManager.GetOrdersByUserId(userId);

            // Assert
            _mockOrderRepository.Verify(x => x.GetOrdersByUserIdDAL(userId), Times.Once);
        }

        [TestMethod]
        public async Task GetOrdersCountAsync_ShouldCallGetOrdersCountAsyncDAL()
        {
            // Arrange
            string filterName = "saoud";
            OrderStatus? filterStatus = OrderStatus.Pending;

            // Act
            await _orderManager.GetOrdersCountAsync(filterName, filterStatus);

            // Assert
            _mockOrderRepository.Verify(x => x.GetOrdersCountAsyncDAL(filterName, filterStatus), Times.Once);
        }

        [TestMethod]
        public async Task GetOrderDataAsync_ShouldCallGetOrderDataAsyncDAL()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string filterName = "saoud";
            OrderStatus? filterStatus = OrderStatus.Pending;

            // Act
            await _orderManager.GetOrderDataAsync(pageNumber, pageSize, filterName, filterStatus);

            // Assert
            _mockOrderRepository.Verify(x => x.GetOrderDataAsyncDAL(pageNumber, pageSize, filterName, filterStatus), Times.Once);
        }

        [TestMethod]
        public async Task UpdateOrdersAsync_ShouldCallUpdateOrdersDAL()
        {
            // Arrange
            var orders = new List<Order>();

            // Act
            await _orderManager.UpdateOrdersAsync(orders);

            // Assert
            _mockOrderRepository.Verify(x => x.UpdateOrdersDAL(orders), Times.Once);
        }

        [TestMethod]
        public async Task GetOrderByIdAsync_ShouldCallGetOrderByIdAsyncDAL()
        {
            // Arrange
            int orderId = 1;

            // Act
            await _orderManager.GetOrderByIdAsync(orderId);

            // Assert
            _mockOrderRepository.Verify(x => x.GetOrderByIdAsyncDAL(orderId), Times.Once);
        }

        [TestMethod]
        public void GetOrdersByUserId_ShouldReturnOrdersFromRepository()
        {
            // Arrange
            int userId = 1;
            var expectedOrders = new List<Order> { new Order(), new Order() };
            _mockOrderRepository.Setup(x => x.GetOrdersByUserIdDAL(userId)).Returns(expectedOrders);

            // Act
            var result = _orderManager.GetOrdersByUserId(userId);

            // Assert
            CollectionAssert.AreEqual(expectedOrders, result);
        }

        [TestMethod]
        public async Task GetOrdersCountAsync_ShouldReturnCountFromRepository()
        {
            // Arrange
            string filterName = "saoud";
            OrderStatus? filterStatus = OrderStatus.Pending;
            int expectedCount = 10;
            _mockOrderRepository.Setup(x => x.GetOrdersCountAsyncDAL(filterName, filterStatus)).ReturnsAsync(expectedCount);

            // Act
            var result = await _orderManager.GetOrdersCountAsync(filterName, filterStatus);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public async Task GetOrderDataAsync_ShouldReturnOrdersFromRepository()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string filterName = "saoud";
            OrderStatus? filterStatus = OrderStatus.Pending;
            var expectedOrders = new List<Order> { new Order(), new Order() };
            _mockOrderRepository.Setup(x => x.GetOrderDataAsyncDAL(pageNumber, pageSize, filterName, filterStatus)).ReturnsAsync(expectedOrders);

            // Act
            var result = await _orderManager.GetOrderDataAsync(pageNumber, pageSize, filterName, filterStatus);

            // Assert
            CollectionAssert.AreEqual(expectedOrders, result);
        }

        [TestMethod]
        public async Task UpdateOrdersAsync_ShouldReturnTrueFromRepository()
        {
            // Arrange
            var orders = new List<Order>();
            _mockOrderRepository.Setup(x => x.UpdateOrdersDAL(orders)).ReturnsAsync(true);

            // Act
            var result = await _orderManager.UpdateOrdersAsync(orders);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetOrderByIdAsync_ShouldReturnOrderFromRepository()
        {
            // Arrange
            int orderId = 1;
            var expectedOrder = new Order { Id = orderId };
            _mockOrderRepository.Setup(x => x.GetOrderByIdAsyncDAL(orderId)).ReturnsAsync(expectedOrder);

            // Act
            var result = await _orderManager.GetOrderByIdAsync(orderId);

            // Assert
            Assert.AreEqual(expectedOrder, result);
        }
    }
}

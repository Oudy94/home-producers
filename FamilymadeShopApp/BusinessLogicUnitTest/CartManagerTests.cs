using BusinessLogicLayer.Managers;
using DataAccessLayer.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLayer.Models;
using Moq;
using System;

namespace BusinessLogicUnitTest
{
    [TestClass]
    public class CartManagerTests
    {
        private Mock<ICartRepository> _mockCartRepository;
        private CartManager _cartManager;

        [TestInitialize]
        public void Setup()
        {
            _mockCartRepository = new Mock<ICartRepository>();
            _cartManager = new CartManager(_mockCartRepository.Object);
        }

        [TestMethod]
        public void AddCart_ValidCart_ShouldCallAddCartDAL()
        {
            // Arrange
            var cart = new Cart();

            // Act
            _cartManager.AddCart(cart);

            // Assert
            _mockCartRepository.Verify(x => x.AddCartDAL(cart), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddCart_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var cart = new Cart();
            _mockCartRepository.Setup(x => x.AddCartDAL(It.IsAny<Cart>())).Throws(new Exception("Test Exception"));

            // Act
            _cartManager.AddCart(cart);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void AddProductToCart_ValidInputs_ShouldCallAddProductToCartDAL()
        {
            // Arrange
            int productId = 1;
            int quantity = 1;
            int customerId = 1;

            // Act
            _cartManager.AddProductToCart(productId, quantity, customerId);

            // Assert
            _mockCartRepository.Verify(x => x.AddProductToCartDAL(productId, quantity, customerId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddProductToCart_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            int productId = 1;
            int quantity = 1;
            int customerId = 1;
            _mockCartRepository.Setup(x => x.AddProductToCartDAL(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception("Test Exception"));

            // Act
            _cartManager.AddProductToCart(productId, quantity, customerId);

            // Assert is handled by ExpectedException
        }
    }
}

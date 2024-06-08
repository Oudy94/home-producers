using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using Moq;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private ProductManager _productManager;

        [TestInitialize]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productManager = new ProductManager(_mockProductRepository.Object);
        }

        [TestMethod]
        public async Task AddProductAsync_ShouldCallAddProductAsyncDAL()
        {
            // Arrange
            var product = new Product();

            // Act
            await _productManager.AddProductAsync(product);

            // Assert
            _mockProductRepository.Verify(x => x.AddProductAsyncDAL(product), Times.Once);
        }

        [TestMethod]
        public void GetProductById_ShouldReturnProductFromRepository()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new Product { Id = productId };
            _mockProductRepository.Setup(x => x.GetProductByIdDAL(productId)).Returns(expectedProduct);

            // Act
            var result = _productManager.GetProductById(productId);

            // Assert
            Assert.AreEqual(expectedProduct, result);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnProductsFromRepository()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string filterName = "chocolate";
            Category? filterCategory = Category.BakeryGoods;
            var expectedProducts = new List<Product> { new Product(), new Product() };
            _mockProductRepository.Setup(x => x.GetAllProductsAsyncDAL(pageNumber, pageSize, filterName, filterCategory)).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productManager.GetAllProductsAsync(pageNumber, pageSize, filterName, filterCategory);

            // Assert
            CollectionAssert.AreEqual(expectedProducts, result);
        }

        [TestMethod]
        public async Task GetProductsCountAsync_ShouldReturnCountFromRepository()
        {
            // Arrange
            string filterName = "chocolate";
            Category? filterCategory = Category.BakeryGoods;
            int expectedCount = 10;
            _mockProductRepository.Setup(x => x.GetProductsCountAsyncDAL(filterName, filterCategory)).ReturnsAsync(expectedCount);

            // Act
            var result = await _productManager.GetProductsCountAsync(filterName, filterCategory);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public async Task UpdateProductsAsync_ShouldReturnTrueFromRepository()
        {
            // Arrange
            var products = new List<Product>();
            _mockProductRepository.Setup(x => x.UpdateProductsAsyncDAL(products)).ReturnsAsync(true);

            // Act
            var result = await _productManager.UpdateProductsAsync(products);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetProductsNamesAsync_ShouldReturnNamesFromRepository()
        {
            // Arrange
            var expectedNames = new List<string> { "Product1", "Product2" };
            _mockProductRepository.Setup(x => x.GetProductsNamesAsyncDAL()).ReturnsAsync(expectedNames);

            // Act
            var result = await _productManager.GetProductsNamesAsync();

            // Assert
            CollectionAssert.AreEqual(expectedNames, result);
        }
    }
}

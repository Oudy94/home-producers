using DataAccessLayer.Interfaces;
using ModelLayer.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Tests
{
    [TestClass]
    public class UserManagerTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserManager _userManager;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userManager = new UserManager(_mockUserRepository.Object);
        }

        [TestMethod]
        public void AddCustomer_ShouldCallAddCustomerDAL()
        {
            // Arrange
            var customer = new Customer();

            // Act
            _userManager.AddCustomer(customer);

            // Assert
            _mockUserRepository.Verify(x => x.AddCustomerDAL(customer), Times.Once);
        }

        [TestMethod]
        public void AddAdmin_ShouldCallAddAdminDAL()
        {
            // Arrange
            var admin = new Admin();

            // Act
            _userManager.AddAdmin(admin);

            // Assert
            _mockUserRepository.Verify(x => x.AddAdminDAL(admin), Times.Once);
        }

        [TestMethod]
        public void GetCustomerById_ShouldReturnCustomerFromRepository()
        {
            // Arrange
            int customerId = 1;
            var expectedCustomer = new Customer { Id = customerId };
            _mockUserRepository.Setup(x => x.GetCustomerByIdDAL(customerId)).Returns(expectedCustomer);

            // Act
            var result = _userManager.GetCustomerById(customerId);

            // Assert
            Assert.AreEqual(expectedCustomer, result);
        }

        [TestMethod]
        public void GetCustomerByCredentials_ShouldReturnCustomerFromRepository()
        {
            // Arrange
            string email = "test@test.com";
            string password = "password";
            var expectedCustomer = new Customer { Email = email, Password = password };
            _mockUserRepository.Setup(x => x.GetCustomerByCredentialsDAL(email, password)).Returns(expectedCustomer);

            // Act
            var result = _userManager.GetCustomerByCredentials(email, password);

            // Assert
            Assert.AreEqual(expectedCustomer, result);
        }

        [TestMethod]
        public void GetAdminByCredentials_ShouldReturnAdminFromRepository()
        {
            // Arrange
            string email = "admin@test.com";
            string password = "123456";
            var expectedAdmin = new Admin { Email = email, Password = password };
            _mockUserRepository.Setup(x => x.GetAdminByCredentialsDAL(email, password)).Returns(expectedAdmin);

            // Act
            var result = _userManager.GetAdminByCredentials(email, password);

            // Assert
            Assert.AreEqual(expectedAdmin, result);
        }

        [TestMethod]
        public async Task GetAllCustomersAsync_ShouldReturnCustomersFromRepository()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string filterName = "Saoud";
            var expectedCustomers = new List<Customer> { new Customer(), new Customer() };
            _mockUserRepository.Setup(x => x.GetAllCustomersAsyncDAL(filterName, pageNumber, pageSize)).ReturnsAsync(expectedCustomers);

            // Act
            var result = await _userManager.GetAllCustomersAsync(filterName, pageNumber, pageSize);

            // Assert
            CollectionAssert.AreEqual(expectedCustomers, result);
        }

        [TestMethod]
        public async Task GetAllAdminsAsync_ShouldReturnAdminsFromRepository()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            string filterName = "Saoud";
            var expectedAdmins = new List<Admin> { new Admin(), new Admin() };
            _mockUserRepository.Setup(x => x.GetAllAdminsAsyncDAL(filterName, pageNumber, pageSize)).ReturnsAsync(expectedAdmins);

            // Act
            var result = await _userManager.GetAllAdminsAsync(filterName, pageNumber, pageSize);

            // Assert
            CollectionAssert.AreEqual(expectedAdmins, result);
        }

        [TestMethod]
        public async Task GetAdminCountAsync_ShouldReturnCountFromRepository()
        {
            // Arrange
            string filterName = "Saoud";
            int expectedCount = 10;
            _mockUserRepository.Setup(x => x.GetAdminCountAsyncDAL(filterName)).ReturnsAsync(expectedCount);

            // Act
            var result = await _userManager.GetAdminCountAsync(filterName);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public async Task GetCustomerCountAsync_ShouldReturnCountFromRepository()
        {
            // Arrange
            string filterName = "Saoud";
            int expectedCount = 10;
            _mockUserRepository.Setup(x => x.GetCustomerCountAsyncDAL(filterName)).ReturnsAsync(expectedCount);

            // Act
            var result = await _userManager.GetCustomerCountAsync(filterName);

            // Assert
            Assert.AreEqual(expectedCount, result);
        }

        [TestMethod]
        public async Task UpdateAdminsAsync_ShouldReturnTrueFromRepository()
        {
            // Arrange
            var admins = new List<Admin>();
            _mockUserRepository.Setup(x => x.UpdateAdminsAsyncDAL(admins)).ReturnsAsync(true);

            // Act
            var result = await _userManager.UpdateAdminsAsync(admins);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateCustomersAsync_ShouldReturnTrueFromRepository()
        {
            // Arrange
            var customers = new List<Customer>();
            _mockUserRepository.Setup(x => x.UpdateCustomersAsyncDAL(customers)).ReturnsAsync(true);

            // Act
            var result = await _userManager.UpdateCustomersAsync(customers);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
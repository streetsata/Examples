using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace DataAccessLibrary.Tests
{
    public class DataAccessTests
    {
        [Fact]
        public void GetEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            var dataAccess = new DataAccess(Mock.Of<IMemoryCache>());

            // Act
            var result = dataAccess.GetEmployees();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetEmployeesAsync_ReturnsListOfEmployeesAsync()
        {
            // Arrange
            var dataAccess = new DataAccess(Mock.Of<IMemoryCache>());

            // Act
            var result = await dataAccess.GetEmployeesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetCachedEmployeesAsync_ReturnsCachedEmployeesIfAvailable()
        {
            // Arrange
            var _dataAccess = new DataAccess(Mock.Of<IMemoryCache>());
            var cachedEmployees = new List<EmployeeModel>
            {
                new() { FirstName = "Serhii", LastName = "Artemenko" },
                new() { FirstName = "Olena", LastName = "Artemenko" },
                new() { FirstName = "Sasha", LastName = "Artemenko" }
            };
            var memoryCacheMock = new Mock<IMemoryCache>();
            memoryCacheMock.Setup(x => x.Get(It.IsAny<EmployeeModel>())).Returns(cachedEmployees);

            // Act
            var result = await _dataAccess.GetCachedEmloyeesAsync();

            // Assert
            result.Should().BeEquivalentTo(cachedEmployees);
        }

        //[Fact]
        //public async Task GetCachedEmployeesAsync_RefreshesCacheIfExpired()
        //{
        //    // Arrange
        //    var employees = new List<EmployeeModel>
        //    {
        //        new EmployeeModel { FirstName = "Test", LastName = "User" }
        //    };

        //    var memoryCacheMock = new Mock<IMemoryCache>();
        //    memoryCacheMock.SetupSequence(x => x.Get<List<EmployeeModel>>("employees"))
        //        .Returns(null)
        //        .Returns(employees);

        //    var dataAccess = new DataAccess(memoryCacheMock.Object);

        //    // Act
        //    var result1 = await dataAccess.GetCachedEmloyeesAsync(); // First call, should populate cache
        //    var result2 = await dataAccess.GetCachedEmloyeesAsync(); // Second call, should return cached value

        //    // Assert
        //    result1.Should().NotBeNull();
        //    result1.Should().BeEquivalentTo(employees);
        //    result2.Should().NotBeNull();
        //    result2.Should().BeEquivalentTo(employees);
        //    memoryCacheMock.Verify(x => x.Set("employees", employees, TimeSpan.FromMinutes(1)), Times.Once);
        //}

        [Fact]
        public async Task GetCachedEmployeesAsync_CachesEmployeesIfNotAvailable()
        {
            // Arrange
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { FirstName = "Test", LastName = "User" }
            };

            var memoryCacheMock = new Mock<IMemoryCache>();
            memoryCacheMock.Setup(x => x.Get<List<EmployeeModel>>("employees")).Returns<List<EmployeeModel>>(valueFunction: null);

            var dataAccess = new DataAccess(memoryCacheMock.Object);

            // Act
            var result = await dataAccess.GetCachedEmloyeesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(employees);
            memoryCacheMock.Verify(x => x.Set("employees", employees, TimeSpan.FromMinutes(1)), Times.Once);
        }
    }
}
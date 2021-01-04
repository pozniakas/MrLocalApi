using Moq;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Helpers;
using MrLocalDb.Entities;
using System;
using Xunit;

namespace MrLocalUnitTests
{
    public class MrLocalValidateFiltersUnitTests
    {
        [Fact]
        public void ValidateData_ValidateFilters_ReturnsTrueIfPassed()
        {
            //Arrange
            var shopRepMock = new Mock<IShopRepository>();
            var userMock = new Mock<IUserRepository>();
            var validateData = new ValidateData(shopRepMock.Object, userMock.Object);
            var shop = new Shop("85bbc04e-8198-4901-bc6d-5446f61098c7", "Shop", "Not Active", "Test", "Berries", "+37065435687", "Vilnius", DateTime.UtcNow, DateTime.UtcNow, "27e2fdc1-cac7-4bfc-b899-d977f526fe77");

            //Act
            var result = validateData.ValidateFilters(shop, "Vilnius", "Berries");

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void ValidateData_ValidateFilters_ReturnsFalseIfNotPassed()
        {
            //Arrange
            var shopRepMock = new Mock<IShopRepository>();
            var userMock = new Mock<IUserRepository>();
            var validateData = new ValidateData(shopRepMock.Object, userMock.Object);
            var shop = new Shop("85bbc04e-8198-4901-bc6d-5446f61098c7", "Shop", "Not Active", "Test", "Berries", "+37065435687", "Vilnius", DateTime.UtcNow, DateTime.UtcNow, "27e2fdc1-cac7-4bfc-b899-d977f526fe77");

            //Act
            var result = validateData.ValidateFilters(shop, "Kaunas", "Other");

            //Assert
            Assert.False(result);
        }
    }
}

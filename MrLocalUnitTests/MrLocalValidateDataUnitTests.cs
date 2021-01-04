using Moq;
using MrLocalBackend.Repositories.Helpers;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Helpers;
using MrLocalDb.Entities;
using System.Threading.Tasks;
using Xunit;

namespace MrLocalUnitTests
{
    public class MrLocalValidateDataUnitTests
    {
        [Theory]
        [InlineData("4564654", "shop", "description", "5.1", false, "Berries")]
        public async Task ValidateData_ValidateProductData_ReturnsTrueIfEnteredFieldsAreValidAsync(string shopId, string name, string description, string priceString, bool isUpdate, string priceType)
        {
            decimal? price = priceString == null ? null : decimal.Parse(priceString);

            //Arange
            var validateShopRepMock = new Mock<IShopRepository>();
            var validateUserMock = new Mock<IUserRepository>();
            var validateData = new ValidateData(validateShopRepMock.Object, validateUserMock.Object);

            //Act
            var result = await validateData.ValidateProductData(shopId, name, description, price, isUpdate, priceType);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(PriceTypes.KILOGRAMS, "KILOGRAMS")]
        [InlineData(PriceTypes.UNIT, "UNIT")]
        [InlineData(PriceTypes.GRAMS, "GRAMS")]
        [InlineData(PriceTypes.UNIT, "KILOGRAMS")]
        public void EnumConverter_StringToPricetype_ReturnsCorrectExpresionOfPriceTypes(PriceTypes realPriceType, string priceType)
        {
            //Arange
            var enumConverter = new EnumConverter();

            //Act
            var result = enumConverter.StringToPricetype(priceType);

            //Assert
            Assert.Equal(realPriceType, result);
        }

    }
}

using Moq;
using Xunit;
using MrLocalBackend.Services.Interfaces;


namespace MrLocalUnitTests
{
    public class MrLocalValidateDataUnitTests
    {
        [Theory]
        [InlineData("4564654", "shop", "description", "5.1" , false, "Berries")]

        public async System.Threading.Tasks.Task ValidateData_ValidateProductData_ReturnsTrueIfEnteredFieldsAreValidAsync(string shopId, string name, string description, string priceString, bool isUpdate, string priceType)
        {
            decimal? price = priceString == null ? null : decimal.Parse(priceString);

            var validateDataMock = new Mock<IValidateData>();

            var result = await validateDataMock.Object.ValidateProductData(shopId, name, description, price, isUpdate, priceType);

            Assert.True(result);
        }
    }
}

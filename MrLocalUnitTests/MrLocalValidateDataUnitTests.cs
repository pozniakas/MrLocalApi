using Moq;
using MrLocalBackend.Repositories.Helpers;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Helpers;
using MrLocalDb.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MrLocalUnitTests
{
    public class MrLocalValidateDataUnitTests
    {
        [Theory]
        [InlineData(PriceTypes.KILOGRAMS, "KILOGRAMS")]
        [InlineData(PriceTypes.UNIT, "UNIT")]
        [InlineData(PriceTypes.GRAMS, "GRAMS")]
        public void EnumConverter_StringToPricetype_ReturnsCorrectExpresionOfPriceTypes(PriceTypes realPriceType, string priceType)
        {
            //Arange
            var enumConverter = new EnumConverter();

            //Act
            var result = enumConverter.StringToPricetype(priceType);

            //Assert
            Assert.Equal(realPriceType, result);
        }
        [Theory]
        [InlineData("AUTOMOBILIS")]
        [InlineData("")]
        [InlineData("GRAMZ")]
        public void EnumConverter_StringToPricetype_ThrowsArgumentException(string priceType)
        {
            //Arange
            var enumConverter = new EnumConverter();

            //Act
            var exception = Assert.Throws<NotImplementedException>(() => enumConverter.StringToPricetype(priceType));

            //Assert
            Assert.Equal("Unknown price type", exception.Message);
        }

    }
}

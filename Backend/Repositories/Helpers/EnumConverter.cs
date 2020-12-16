using Backend.Repositories.Interfaces;
using System;
using static Backend.Models.Product;

namespace Backend.Repositories.Helpers
{
    public class EnumConverter : IEnumConverter
    {
        public PriceTypes StringToPricetype(string pricetype)
        {
            return pricetype switch
            {
                "GRAMS" => PriceTypes.GRAMS,
                "KILOGRAMS" => PriceTypes.KILOGRAMS,
                "UNIT" => PriceTypes.UNIT,
                _ => throw new NotImplementedException("Unknown price type")
            };
        }
    }
}

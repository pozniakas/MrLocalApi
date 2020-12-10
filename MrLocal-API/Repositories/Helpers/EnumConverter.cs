using MrLocal_API.Repositories.Interfaces;
using System;
using static MrLocal_API.Models.Product;

namespace MrLocal_API.Repositories.Helpers
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

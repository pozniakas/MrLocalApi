using System;
using static MrLocal_Backend.Models.ProductModel;

namespace MrLocal_Backend.Repositories.Helpers
{
    public class EnumConverter
    {
        public PriceTypes StringToPricetype(string pricetype)
        {
            return pricetype switch
            {
                "GRAMS" => PriceTypes.GRAMS,
                "KILOGRAMS" => PriceTypes.KILOGRAMS,
                "UNIT" => PriceTypes.UNIT,
                _ => throw new NotImplementedException()
            };
        }
    }
}

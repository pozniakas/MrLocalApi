using System;

namespace MrLocal_Backend.Repositories.Helpers
{
    public class EnumConverter
    {
        public ProductRepository.PriceTypes StringToPricetype(string pricetype)
        {
            return pricetype switch
            {
                "GRAMS" => ProductRepository.PriceTypes.GRAMS,
                "KILOGRAMS" => ProductRepository.PriceTypes.KILOGRAMS,
                "UNIT" => ProductRepository.PriceTypes.UNIT,
                _ => throw new NotImplementedException()
            };
        }
    }
}

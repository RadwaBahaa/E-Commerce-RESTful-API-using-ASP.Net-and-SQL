using DTOs.Validation;

namespace DTOs.DTOs.Products
{
    public class CreateOrUpdateProductDTO
    {
        public string Name { get; set; }
        [ProductPriceValidation]
        public float Price { get; set; }
        public string? Description { get; set; }
        public int CategoryID { get; set; }
    }
}

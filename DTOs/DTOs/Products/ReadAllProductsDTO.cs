using DTOs.DTOs.Category;

namespace DTOs.DTOs.Products
{
    public class ReadAllProductsDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public string VendorID { get; set; }
        public ReadAllCategoryDTO Category { get; set; }
    }
}

namespace DTOs.DTOs.Products
{
    public class ReadProductsByCategoryDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public string VendorID { get; set; }
    }
}

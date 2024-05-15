namespace Models.Models
{
    public class Product
    {
        // Product properties ................
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        public int? CategoryID { get; set; }
        public string VendorID { get; set; }

        // Relations .........................
        public Category Category { get; set; }
        public List<OrderProduct> Products { get; set; }
        public User Vendor { get; set; }
    }
}

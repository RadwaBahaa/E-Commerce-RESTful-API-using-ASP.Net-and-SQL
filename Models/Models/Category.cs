namespace Models.Models
{
    public class Category
    {
        // Category properties ...............
        public int CategoryID { get; set; }
        public string Name { get; set; }

        // Relations .........................
        public List<Product> Products { get; set; }
    }
}

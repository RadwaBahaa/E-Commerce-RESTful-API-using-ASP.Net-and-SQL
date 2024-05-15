namespace Models.Models
{
    public class Order
    {
        // Order properties ..................
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string Status { get; set; }

        // Relations .........................
        public User Customer { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}

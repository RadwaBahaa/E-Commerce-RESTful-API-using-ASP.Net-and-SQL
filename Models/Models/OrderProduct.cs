namespace Models.Models
{
    public class OrderProduct
    {
        // OrderProduct properties ..................
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }

        // Relations .........................
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}

using DTOs.DTOs.OrderProducts;

namespace DTOs.DTOs.Orders
{
    public class ReadAllOrdersDTO
    {
        public int OrderID { get; set; }
        public string Status { get; set; }
        public string CustomerID { get; set; }
        public List<ReadOrCreateOrderProductDTO> Products { get; set; }
    }
}

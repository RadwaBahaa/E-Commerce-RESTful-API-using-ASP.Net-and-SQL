using DTOs.Validation;

namespace DTOs.DTOs.OrderProducts
{
    public class ReadOrCreateOrderProductDTO
    {
        public int ProductID { get; set; }
        [ProductQuantityValidation]
        public int ProductQuantity { get; set; }
    }
}

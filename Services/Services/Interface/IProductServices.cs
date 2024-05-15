using DTOs.DTOs.Products;

namespace Services.Services.Interface
{
    public interface IProductServices
    {
        public Task<ReadAllProductsDTO> Create(CreateOrUpdateProductDTO productDTO, string vendorID);
        public Task<List<ReadAllProductsDTO>> GetAll();
        public Task<List<ReadProductsByCategoryDTO>> GetbyCategory(string categoryName);
        public Task<ReadAllProductsDTO> GetOne(int ID);
        public Task<ReadAllProductsDTO> Update(CreateOrUpdateProductDTO productDTO, int ID, string vendorID);
        public Task<bool> Delete(int ID, string vendorID);
    }
}

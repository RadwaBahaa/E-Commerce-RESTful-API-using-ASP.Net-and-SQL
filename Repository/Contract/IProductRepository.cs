using Models.Models;

namespace Repository.Contract
{
    public interface IProductRepository : IGenaricRepository<Product>
    {
        public Task<Product> GetOneByID(int ID);
        public Task<Product> GetLastProduct();
    }
}

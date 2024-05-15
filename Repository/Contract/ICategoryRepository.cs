using Models.Models;

namespace Repository.Contract
{
    public interface ICategoryRepository : IGenaricRepository<Category>
    {
        public Task<Category> GetOneByID(int ID);
        public Task<bool> GetOneByName(string categoryName);
    }
}

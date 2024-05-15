using Models.Models;

namespace Repository.Contract
{
    public interface IOrderRepository : IGenaricRepository<Order>
    {
        public Task<Order> GetOneByID(int ID);
        public Task<Order> GetLastOrder();
    }
}

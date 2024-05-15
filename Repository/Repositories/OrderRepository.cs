using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Contract;

namespace Repository.Repositories
{
    public class OrderRepository : GenaricRepository<Order>, IOrderRepository
    {
        protected EcommerceDbContext context { get; set; }
        public OrderRepository(EcommerceDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Order> GetOneByID(int ID)
        {
            var order = context.Orders.Include(o=>o.Products).FirstOrDefault(o=>o.OrderID == ID);
            return order != null? order : null;
        }
        public async Task<Order> GetLastOrder()
        {
            var lastOrder = context.Orders.OrderByDescending(o=>o.OrderID).FirstOrDefault();
            return lastOrder;
        }
    }
}

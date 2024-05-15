using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Contract;

namespace Repository.Repositories
{
    public class ProductRepository : GenaricRepository<Product>, IProductRepository
    {
        protected EcommerceDbContext context { get; set; }
        public ProductRepository(EcommerceDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Product> GetOneByID(int ID)
        {
            var product = context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductID == ID);
            return product != null ? product : null;
        }
        public async Task<Product> GetLastProduct()
        {
            var lastProduct = context.Products.OrderByDescending(o => o.ProductID).FirstOrDefault();
            return lastProduct;
        }
    }
}

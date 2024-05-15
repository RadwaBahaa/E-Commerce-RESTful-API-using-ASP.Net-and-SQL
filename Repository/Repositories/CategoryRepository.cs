using Context.Context;
using Models.Models;
using Repository.Contract;

namespace Repository.Repositories
{
    public class CategoryRepository : GenaricRepository<Category>, ICategoryRepository
    {
        protected EcommerceDbContext context;
        public CategoryRepository(EcommerceDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Category> GetOneByID(int ID)
        {
            var category = context.Categories.FirstOrDefault(c => c.CategoryID == ID);
            return category;
        }
        public async Task<bool> GetOneByName(string categoryName)
        {
            var category = context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryName.ToLower());
            return category != null;
        }
    }
}

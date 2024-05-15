using Context.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Contract;

namespace Repository.Repositories
{
    public class GenaricRepository<Model> : IGenaricRepository<Model> where Model : class
    {
        protected EcommerceDbContext context;
        protected DbSet<Model> models;
        public GenaricRepository(EcommerceDbContext context)
        {
            this.context = context;
            models = context.Set<Model>();
        }
        public async Task Create(Model model)
        {
            await models.AddAsync(model);
            await context.SaveChangesAsync();
        }
        public async Task<IQueryable<Model>> GetAll()
        {
            return await Task.FromResult(models.Select(m => m));
        }
        public async Task Update()
        {
            await context.SaveChangesAsync();
        }
        public async Task Delete(Model model)
        {
            models.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}

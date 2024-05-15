namespace Repository.Contract
{
    public interface IGenaricRepository<Model>
    {
        public Task Create(Model model);
        public Task<IQueryable<Model>> GetAll();
        public Task Update();
        public Task Delete(Model model);
    }
}

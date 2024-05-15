using AutoMapper;
using DTOs.DTOs.Category;
using Models.Models;
using Repository.Contract;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class CategoryServices : ICategoryServices
    {
        protected ICategoryRepository categoryrepository;
        protected IMapper mapper;
        public CategoryServices(ICategoryRepository categoryrepository, IMapper mapper)
        {
            this.categoryrepository = categoryrepository;
            this.mapper = mapper;
        }
        // _________________________ Create a new Category _________________________
        public async Task<ReadAllCategoryDTO> Create(CreateOrUpdateCategoryDTO categoryDTO)
        {
            var findCategory = await categoryrepository.GetOneByName(categoryDTO.Name);

            // Find out whether any categories have the same name.
            if (findCategory)
            {
                return null;
            }
            else
            {
                // Make the initial letter of the Category name capitalized.
                categoryDTO.Name = char.ToUpper(categoryDTO.Name[0]) + categoryDTO.Name.Substring(1).ToLower();
                // Create a new Category.
                var createCategory = mapper.Map<Category>(categoryDTO);
                await categoryrepository.Create(createCategory);
                // Find the Category after creation.
                var categories = await categoryrepository.GetAll();
                var findCreatedCategory = categories.OrderByDescending(o => o.CategoryID).FirstOrDefault();
                var mappingCategory = mapper.Map<ReadAllCategoryDTO>(findCreatedCategory);
                return mappingCategory;
            }
        }
        // ___________________________ Get All Categories ___________________________
        public async Task<List<ReadAllCategoryDTO>> GetAll()
        {
            var categories = await categoryrepository.GetAll();
            var mappedCategories = categories.Select(c => mapper.Map<ReadAllCategoryDTO>(c));
            var categoriesList = mappedCategories.ToList();
            return categoriesList;
        }
        // ___________________________ Get One Categories ___________________________
        public async Task<ReadAllCategoryDTO> GetOne(int ID)
        {
            var findCategory = await categoryrepository.GetOneByID(ID);
            if (findCategory != null)
            {
                var mappedCategory = mapper.Map<ReadAllCategoryDTO>(findCategory);
                return mappedCategory;
            }
            else
            {
                return null;
            }
        }
        // __________________________ Update a Categories ___________________________
        public async Task<bool> Update(CreateOrUpdateCategoryDTO categoryDTO, int ID)
        {
            var findCategory = await categoryrepository.GetOneByID(ID);
            if (findCategory == null)
            {
                return false;
            }
            else
            {
                // Make the initial letter of the Category name capitalized.
                categoryDTO.Name = char.ToUpper(categoryDTO.Name[0]) + categoryDTO.Name.Substring(1).ToLower();
                // Update the Category.
                mapper.Map(categoryDTO, findCategory);
                await categoryrepository.Update();
                return true;
            }
        }
        // __________________________ Delete a Categories ___________________________
        public async Task<bool> Delete(int ID)
        {
            var findCategory = await categoryrepository.GetOneByID(ID);
            if (findCategory == null)
            {
                return false;
            }
            else
            {
                await categoryrepository.Delete(findCategory);
                return true;
            }
        }
    }
}

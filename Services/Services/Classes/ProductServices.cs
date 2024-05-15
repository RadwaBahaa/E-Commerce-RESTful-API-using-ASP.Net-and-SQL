using AutoMapper;
using DTOs.DTOs.Products;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Contract;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class ProductServices : IProductServices
    {
        protected IProductRepository productRepository;
        protected ICategoryRepository categoryRepository;
        protected IMapper mapper;
        public ProductServices(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        // _________________________ Create a new Product _________________________
        public async Task<ReadAllProductsDTO> Create(CreateOrUpdateProductDTO productDTO, string vendorID)
        {
            var findCategory = await categoryRepository.GetOneByID(productDTO.CategoryID);
            if (findCategory != null)
            {
                productDTO.Name = char.ToUpper(productDTO.Name[0]) + productDTO.Name.Substring(1).ToLower();
                Product newProduct = mapper.Map<Product>(productDTO);
                newProduct.VendorID = vendorID;
                await productRepository.Create(newProduct);
                var getCreatedProduct = await productRepository.GetLastProduct();
                return mapper.Map<ReadAllProductsDTO>(getCreatedProduct);
            }
            else
            {
                return null;
            }
        }
        // ___________________________ Get All Products ___________________________
        public async Task<List<ReadAllProductsDTO>> GetAll()
        {
            var products = await productRepository.GetAll();
            var getAllProducts = products.Include(p => p.Category).Select(p => mapper.Map<ReadAllProductsDTO>(p)).ToList();
            return getAllProducts;
        }
        // ________________ Get Products filtered by category name ________________
        public async Task<List<ReadProductsByCategoryDTO>> GetbyCategory(string categoryName)
        {
            var findCategory = await categoryRepository.GetOneByName(categoryName);
            if (findCategory)
            {
                var allProducts = await productRepository.GetAll();
                var selectProducts = allProducts
                    .Include(p => p.Category)
                    .Where(p => p.Category.Name.ToLower() == categoryName.ToLower())
                    .Select(c => mapper.Map<ReadProductsByCategoryDTO>(c))
                    .ToList();
                return selectProducts;
            }
            else
            {
                return null;
            }
        }
        // ___________________________ Get One Products ___________________________
        public async Task<ReadAllProductsDTO> GetOne(int ID)
        {
            var product = await productRepository.GetOneByID(ID);
            if (product != null)
            {
                return mapper.Map<ReadAllProductsDTO>(product);
            }
            else
            {
                return null;
            }
        }
        // __________________________ Update a Products ___________________________
        public async Task<ReadAllProductsDTO> Update(CreateOrUpdateProductDTO productDTO, int ID, string vendorID)
        {
            var product = await productRepository.GetOneByID(ID);
            var findCategory = await categoryRepository.GetOneByID(productDTO.CategoryID);
            if (product == null || findCategory == null)
                return null;
            else
            {
                if (vendorID != null)
                {
                    if (product.VendorID != vendorID)
                        return null;
                    else product.Status = "Updated";
                }
                else product.Status = "Updated";
                productDTO.Name = char.ToUpper(productDTO.Name[0]) + productDTO.Name.Substring(1).ToLower();
                mapper.Map(productDTO, product);
                await productRepository.Update();
                return mapper.Map<ReadAllProductsDTO>(product);
            }
        }
        // __________________________ Delete a Products ___________________________
        public async Task<bool> Delete(int ID, string vendorID)
        {
            var product = await productRepository.GetOneByID(ID);
            if (product != null)
                if (vendorID == null)
                {
                    await productRepository.Delete(product);
                    return true;
                }
                else
                {
                    if (product.VendorID == vendorID)
                    {
                        await productRepository.Delete(product);
                        return true;
                    }
                    else return false;
                }
            else return false;
        }
    }
}

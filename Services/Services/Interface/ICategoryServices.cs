using AutoMapper;
using DTOs.DTOs.Category;
using Models.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface ICategoryServices
    {
        public Task<ReadAllCategoryDTO> Create(CreateOrUpdateCategoryDTO categoryDTO);
        public Task<List<ReadAllCategoryDTO>> GetAll();
        public Task<ReadAllCategoryDTO> GetOne(int ID);
        public Task<bool> Update(CreateOrUpdateCategoryDTO categoryDTO, int ID);
        public Task<bool> Delete(int ID);
    }
}

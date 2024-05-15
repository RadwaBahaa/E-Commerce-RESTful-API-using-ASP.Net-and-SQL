using AutoMapper;
using DTOs.DTOs.Orders;
using Models.Models;
using Repository.Repositories;

namespace Services.Services.Interface
{
    public interface IOrderServices
    {
        public Task<ReadUpdatedorCreaterOrderDTO> Create(CreateOrderDTO orderDTO, string customerID);
        public Task<List<ReadAllOrdersDTO>> GetAll(string userRole, string userID);
        public Task<ReadAllOrdersDTO> GetOne(int ID, string userRole, string userID);
        public Task<ReadUpdatedorCreaterOrderDTO> Update(UpdateOrderDTO orderDTO, int ID);
        public Task<bool> Delete(int ID, string userID);
    }
}

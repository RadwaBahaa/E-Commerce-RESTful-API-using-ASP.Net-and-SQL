using AutoMapper;
using DTOs.DTOs.Orders;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Contract;
using Services.Services.Interface;


namespace Services.Services.Classes
{
    public class OrderServices : IOrderServices
    {
        protected IOrderRepository orderRepository;
        protected IProductRepository productRepository;
        protected IMapper mapper;
        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        // _________________________ Create a new Order _________________________
        public async Task<ReadUpdatedorCreaterOrderDTO> Create(CreateOrderDTO orderDTO, string customerID)
        {
            var orderedProducts = orderDTO.Products;
            foreach (var product in orderedProducts)
            {
                var findProduct = await productRepository.GetOneByID(product.ProductID);
                if (findProduct == null)
                {
                    return null;
                }
            }
            var createOrder = mapper.Map<Order>(orderDTO);
            createOrder.CustomerID = customerID;
            await orderRepository.Create(createOrder);
            var getCreatedOrder = await orderRepository.GetLastOrder();
            var mappingOrder = mapper.Map<ReadUpdatedorCreaterOrderDTO>(getCreatedOrder);
            return mappingOrder;
        }
        // ___________________________ Get All Orders ___________________________
        public async Task<List<ReadAllOrdersDTO>> GetAll(string userRole, string userID)
        {
            var orders = await orderRepository.GetAll();
            switch (userRole)
            {
                case "User":
                    var userOrders = orders
                        .Include(o => o.Products)
                        .Where(o => o.CustomerID == userID)
                        .Select(o => mapper.Map<ReadAllOrdersDTO>(o))
                        .ToList();
                    return userOrders;
                case "Vendor":
                    var vendorOrders = new List<Order>();
                    var orderList = orders.Include(o => o.Products).ToList();
                    foreach (var order in orderList)
                    {
                        var vendorProductOrders = new List<OrderProduct>();
                        foreach (var product in order.Products)
                        {
                            var findProductsOrVendor = await productRepository.GetOneByID(product.ProductID);
                            if (findProductsOrVendor.VendorID == userID)
                                vendorProductOrders.Add(product);
                        }
                        if (vendorProductOrders.Count > 0)
                        {
                            order.Products = vendorProductOrders;
                            vendorOrders.Add(order);
                        }
                    }
                    return mapper.Map<List<ReadAllOrdersDTO>>(vendorOrders);
                case "Admin":
                    var allorders = orders.Include(o => o.Products).Select(o => mapper.Map<ReadAllOrdersDTO>(o)).ToList();
                    return allorders;
            }
            return null;
        }
        // ___________________________ Get One Order ___________________________
        public async Task<ReadAllOrdersDTO> GetOne(int ID, string userRole, string? userID)
        {
            var order = await orderRepository.GetOneByID(ID);
            var mappingOrder = mapper.Map<ReadAllOrdersDTO>(order);
            switch (userRole)
            {
                case "User":
                    if (order.CustomerID == userID)
                        return mappingOrder;
                    break;
                case "Vendor":
                    var vendorProductOrders = new List<OrderProduct>();
                    foreach (var product in order.Products)
                    {
                        var findProductsOrVendor = await productRepository.GetOneByID(product.ProductID);
                        if (findProductsOrVendor.VendorID == userID)
                            vendorProductOrders.Add(product);
                    }
                    if (vendorProductOrders.Count == 0) return null;
                    order.Products = vendorProductOrders;

                    return mapper.Map<ReadAllOrdersDTO>(order);
                case "Admin":
                    return mappingOrder;
            }
            return null;
        }
        // __________________________ Update a Order ___________________________
        public async Task<ReadUpdatedorCreaterOrderDTO> Update(UpdateOrderDTO orderDTO, int ID)
        {
            var findOrder = await orderRepository.GetOneByID(ID);
            if (findOrder != null)
            {
                orderDTO.Status = char.ToUpper(orderDTO.Status[0]) + orderDTO.Status.Substring(1);
                mapper.Map(orderDTO, findOrder);
                await orderRepository.Update();
                var printUpdatedOrder = mapper.Map<ReadUpdatedorCreaterOrderDTO>(findOrder);
                return printUpdatedOrder;
            }
            else
            {
                return null;
            }

        }
        // __________________________ Delete a Order ___________________________
        public async Task<bool> Delete(int ID, string userID)
        {
            var findOrder = await orderRepository.GetOneByID(ID);
            if (findOrder != null)
            {
                if (userID == null)
                {
                    await orderRepository.Delete(findOrder);
                    return true;
                }
                else
                {
                    if (findOrder.CustomerID == userID)
                    {
                        await orderRepository.Delete(findOrder);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}

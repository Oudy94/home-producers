using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IOrderManager
    {
        void AddOrder(Order order);
        List<Order> GetOrdersByUserId(int id);
        Task<int> GetOrdersCountAsync(int userId, OrderStatus? filterStatus);
        Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize, int userId, OrderStatus? filterStatus);
        Task<bool> UpdateOrdersAsync(List<Order> orders);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}

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
        Task<int> GetOrdersCountAsync(string filterName, OrderStatus? filterStatus);
        Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize, string filterName, OrderStatus? filterStatus);
        Task<bool> UpdateOrdersAsync(List<Order> orders);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}

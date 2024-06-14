﻿using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrderDAL(Order order);
        List<Order> GetOrdersByUserIdDAL(int customerId);
        Task<int> GetOrdersCountAsyncDAL(int userId, OrderStatus? filterStatus);
        Task<List<Order>> GetOrdersAsyncDAL(int pageNumber, int pageSize, int userId, OrderStatus? filterStatus);
        Task<bool> UpdateOrdersDAL(List<Order> orders);
        Task<Order> GetOrderByIdAsyncDAL(int orderId);
    }
}

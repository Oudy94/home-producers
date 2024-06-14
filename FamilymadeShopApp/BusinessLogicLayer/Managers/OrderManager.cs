using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Interfaces;
using ModelLayer.Models;
using DataAccessLayer.Interfaces;
using SharedLayer.Enums;

namespace BusinessLogicLayer.Managers
{
    public class OrderManager: IOrderManager
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public void AddOrder(Order order)
        {
            try
            {
                _orderRepository.AddOrderDAL(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Order> GetOrdersByUserId(int id)
        {
            try
            {
                return _orderRepository.GetOrdersByUserIdDAL(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public async Task<int> GetOrdersCountAsync(int userId, OrderStatus? filterStatus)
		{
			try
			{
				return await _orderRepository.GetOrdersCountAsyncDAL(userId, filterStatus);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize, int userId, OrderStatus? filterStatus)
		{
			try
			{
				return await _orderRepository.GetOrdersAsyncDAL(pageNumber, pageSize, userId, filterStatus);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateOrdersAsync(List<Order> orders)
		{
			try
			{
				return await _orderRepository.UpdateOrdersDAL(orders);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Order> GetOrderByIdAsync(int orderId)
		{
			try
			{
				return await _orderRepository.GetOrderByIdAsyncDAL(orderId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

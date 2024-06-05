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
    public class OrderManager : IManager<Order>
    {
        private readonly OrderRepository _OrderRepository;

        public OrderManager()
        {
            this._OrderRepository = new OrderRepository();
        }

        public void Add(Order order)
        {
            try
            {
                _OrderRepository.AddOrderToDB(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByUserId(int id)
        {
            try
            {
                return _OrderRepository.GetOrdersByUserIdFromDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public async Task<int> GetOrdersCountAsync(string filterName, OrderStatus? filterStatus)
		{
			try
			{
				return await _OrderRepository.GetOrdersCountDBAsync(filterName, filterStatus);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Order>> GetOrderDataAsync(int pageNumber, int pageSize, string filterName, OrderStatus? filterStatus)
		{
			try
			{
				return await _OrderRepository.GetOrderDataDBAsync(pageNumber, pageSize, filterName, filterStatus);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateOrderData(List<Order> orders)
		{
			try
			{
				return await _OrderRepository.UpdateOrderDataDBAsync(orders);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Order> GetOrderAsync(int orderId)
		{
			try
			{
				return await _OrderRepository.GetOrderDBAsync(orderId);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

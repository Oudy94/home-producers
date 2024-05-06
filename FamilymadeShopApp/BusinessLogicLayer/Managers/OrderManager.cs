using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Interfaces;
using ModelLayer.Models;

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
    }
}

using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrderToDB(Order order);
        List<Order> GetOrdersByUserIdFromDB(int id);
    }
}

using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        void AddUserToDB(Customer user);
        Customer GetUserFromDB(int id);
        List<Customer> GetCustomersFromDB();
        Customer AuthenticateCustomerFromDB(string email, string password);
    }
}

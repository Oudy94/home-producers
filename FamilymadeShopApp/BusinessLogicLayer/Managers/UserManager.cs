using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Interfaces;
using ModelLayer.Models;

namespace BusinessLogicLayer.Managers
{
    public class UserManager : IManager<Customer>
    {
        private readonly UserRepository _UserRepository;

        public UserManager()
        {
            this._UserRepository = new UserRepository();
        }

        public void Add(Customer user)
        {
            try
            {
                _UserRepository.AddUserToDB(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer Get(int id)
        {
            try
            {
                return _UserRepository.GetUserFromDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer AuthenticateCustomer(string email, string password)
        {
            try
            {
                return _UserRepository.AuthenticateCustomerFromDB(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

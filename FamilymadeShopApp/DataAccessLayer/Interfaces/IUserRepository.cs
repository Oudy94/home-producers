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
        void AddCustomerDAL(Customer user);
        void AddAdminDAL(Admin admin);
        Customer GetCustomerByIdDAL(int id);
        Task<List<Customer>> GetAllCustomersAsyncDAL(string filterName, int pageNumber, int pageSize);
        Task<List<Admin>> GetAllAdminsAsyncDAL(string filterName, int pageNumber, int pageSize);
        Task<int> GetAdminCountAsyncDAL(string filterName);
        Task<int> GetCustomerCountAsyncDAL(string filterName);
        Customer GetCustomerByCredentialsDAL(string email, string password);
        Admin GetAdminByCredentialsDAL(string email, string password);
        Task<bool> UpdateAdminsAsyncDAL(List<Admin> admins);
        Task<bool> UpdateCustomersAsyncDAL(List<Customer> customers);
    }
}

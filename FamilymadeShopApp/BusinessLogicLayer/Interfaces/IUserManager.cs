using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserManager
    {
        void AddCustomer(Customer user);
        void AddAdmin(Admin admin);
        Customer GetCustomerById(int id);
        Customer GetCustomerByCredentials(string email, string password);
        Admin GetAdminByCredentials(string email, string password);
        Task<List<Customer>> GetAllCustomersAsync(string filterName, int pageNumber, int pageSize);
        Task<List<Admin>> GetAllAdminsAsync(string filterName, int pageNumber, int pageSize);
        Task<int> GetAdminCountAsync(string filterName);
        Task<int> GetCustomerCountAsync(string filterName);
        Task<bool> UpdateAdminsAsync(List<Admin> admins);
        Task<bool> UpdateCustomersAsync(List<Customer> customers);
        Task RemoveUserByIdAsync(int id);
    }
}

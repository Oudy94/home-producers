using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Interfaces;
using ModelLayer.Models;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Managers
{
	public class UserManager: IUserManager
    {
		private readonly IUserRepository _userRepository;

		public UserManager(IUserRepository userRepository)
		{
			this._userRepository = userRepository;
		}

		public void AddCustomer(Customer user)
		{
			try
			{
				_userRepository.AddCustomerDAL(user);
			}
            catch (ExistingEmailException ex)
            {
                throw new ExistingEmailException(ex.Message, ex);
            }
            catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public void AddAdmin(Admin admin)
		{
			try
			{
				_userRepository.AddAdminDAL(admin);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Customer GetCustomerById(int id)
		{
			try
			{
				return _userRepository.GetCustomerByIdDAL(id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public Customer GetCustomerByCredentials(string email, string password)
        {
            try
            {
                return _userRepository.GetCustomerByCredentialsDAL(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Admin GetAdminByCredentials(string email, string password)
        {
            try
            {
                return _userRepository.GetAdminByCredentialsDAL(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync(string filterName, int pageNumber, int pageSize)
		{
			try
			{
				return await _userRepository.GetAllCustomersAsyncDAL(filterName, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Admin>> GetAllAdminsAsync(string filterName, int pageNumber, int pageSize)
		{
			try
			{
				return await _userRepository.GetAllAdminsAsyncDAL(filterName, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> GetAdminCountAsync(string filterName)
		{
			try
			{
				return await _userRepository.GetAdminCountAsyncDAL(filterName);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> GetCustomerCountAsync(string filterName)
		{
			try
			{
				return await _userRepository.GetCustomerCountAsyncDAL(filterName);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateAdminsAsync(List<Admin> admins)
		{
			try
			{
				return await _userRepository.UpdateAdminsAsyncDAL(admins);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateCustomersAsync(List<Customer> customers)
		{
			try
			{
				return await _userRepository.UpdateCustomersAsyncDAL(customers);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task RemoveUserByIdAsync(int id)
		{
			try
			{
				await _userRepository.RemoveUserByIdAsyncDAL(id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

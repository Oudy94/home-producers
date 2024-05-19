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

		public void AddAdmin(Admin admin)
		{
			try
			{
				_UserRepository.AddAdminToDB(admin);
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

		public async Task<List<Customer>> GetCustomerDataAsync(string filterName, int pageNumber, int pageSize)
		{
			try
			{
				return await _UserRepository.GetCustomersFromDBAsync(filterName, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Admin>> GetAdminDataAsync(string filterName, int pageNumber, int pageSize)
		{
			try
			{
				return await _UserRepository.GetAdminsFromDBAsync(filterName, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> GetAdminDataCountAsync(string filterName)
		{
			try
			{
				return await _UserRepository.GetAdminDataCountFromDBAsync(filterName);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<int> GetCustomerDataCountAsync(string filterName)
		{
			try
			{
				return await _UserRepository.GetCustomerDataCountFromDBAsync(filterName);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
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

		public Admin AuthenticateAdmin(string email, string password)
		{
			try
			{
				return _UserRepository.AuthenticateAdminFromDB(email, password);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateAdminDataAsync(List<Admin> admins)
		{
			try
			{
				return await _UserRepository.UpdateAdminDataDBAsync(admins);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> AddAdminData(List<Admin> admins)
		{
			try
			{
				return await _UserRepository.AddAdminDataDBAsync(admins);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateCustomerData(List<Customer> customers)
		{
			try
			{
				return await _UserRepository.UpdateCustomerDataDBAsync(customers);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

﻿using System;
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
		private readonly DatabaseHelper _dbHelper;

		public UserManager()
		{
			this._dbHelper = new DatabaseHelper();
		}

		public void Add(Customer user)
		{
			try
			{
				_dbHelper.OpenConnection();
				_dbHelper.AddUserToDB(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				_dbHelper.CloseConnection();
			}
		}

		public Customer Get(int id)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.GetUserFromDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
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
                _dbHelper.OpenConnection();
                return _dbHelper.AuthenticateCustomerFromDB(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _dbHelper.CloseConnection();
            }
        }
    }
}
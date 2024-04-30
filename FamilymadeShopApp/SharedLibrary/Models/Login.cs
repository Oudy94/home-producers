﻿using SharedLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Login
    {
        private readonly DatabaseHelper _dbHelper;

        public Login()
        {
            this._dbHelper = new DatabaseHelper();
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

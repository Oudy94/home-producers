using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Helpers;
using SharedLibrary.Interfaces;

namespace SharedLibrary.Models
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
            throw new NotImplementedException();
        }

        public List<Customer> GetAll() 
        { 
            throw new NotImplementedException();
        }
    }
}

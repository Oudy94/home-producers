using SharedLayer.Enums;
using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace BusinessLogicLayer.Managers
{
    public class ProductManager: IManager<Product>
	{
		private readonly DatabaseHelper _dbHelper;

		public ProductManager()
		{
			this._dbHelper = new DatabaseHelper();
		}

		public void Add(Product product)
        {
			throw new NotImplementedException();

			try
			{
				_dbHelper.OpenConnection();
				//_dbHelper.AddProductToDB(product);
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

        public Product Get(int id)
        {
			try
			{
				_dbHelper.OpenConnection();
				return _dbHelper.GetProductByIdFromDB(id);
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

        public List<Product> GetAll()
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.GetProductsFromDB();
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

        public List<Product> GetAll(string searchTerm, int pageNumber = 1)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.GetProductsFromDB(searchTerm, pageNumber);
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

        public int GetProductsCount(string searchTerm)
        {
            try
            {
                _dbHelper.OpenConnection();
                return _dbHelper.GetProductsCountFromDB(searchTerm);
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

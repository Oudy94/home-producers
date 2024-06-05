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
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Managers
{
    public class ProductManager : IManager<Product>
    {
        private readonly ProductRepository _ProductRepository;

        public ProductManager()
        {
            this._ProductRepository = new ProductRepository();
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            try
            {
                return _ProductRepository.GetProductByIdFromDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                return _ProductRepository.GetProductsFromDB();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Product> GetAll(string filterName, int pageNumber = 1)
        {
            try
            {
                return _ProductRepository.GetProductsFromDB(filterName, pageNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetProductsCount(string filterName, Category? filterCategory)
        {
            try
            {
                return await _ProductRepository.GetProductsCountDBAsync(filterName, filterCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product>> GetProductDataAsync(int pageNumber, int pageSize, string filterName, Category? filterCategory)
        {
			try
			{
				return await _ProductRepository.GetProductDataDBAsync(pageNumber, pageSize, filterName, filterCategory);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<bool> UpdateProductData(List<Product> products)
		{
			try
			{
				return await _ProductRepository.UpdateProductDataDBAsync(products);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task AddProductAsync(Product product)
		{
			try
			{
				await _ProductRepository.AddProductDBAsync(product);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public async Task<List<string>> GetProductsNamesAsync()
        {
            try
            {
                return await _ProductRepository.GetProductsNamesDBAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

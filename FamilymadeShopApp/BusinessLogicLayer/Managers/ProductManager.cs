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
    public class ProductManager: IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _productRepository.AddProductAsyncDAL(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                return _productRepository.GetProductByIdDAL(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product>> GetAllProductsAsync(int pageNumber, int pageSize, string filterName, Category? filterCategory)
        {
            try
            {
                return await _productRepository.GetAllProductsAsyncDAL(pageNumber, pageSize, filterName, filterCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetProductsCountAsync(string filterName, Category? filterCategory)
        {
            try
            {
                return await _productRepository.GetProductsCountAsyncDAL(filterName, filterCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public async Task<bool> UpdateProductsAsync(List<Product> products)
		{
			try
			{
				return await _productRepository.UpdateProductsAsyncDAL(products);
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
                return await _productRepository.GetProductsNamesAsyncDAL();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

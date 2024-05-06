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

        public List<Product> GetAll(string searchTerm, int pageNumber = 1)
        {
            try
            {
                return _ProductRepository.GetProductsFromDB(searchTerm, pageNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetProductsCount(string searchTerm)
        {
            try
            {
                return _ProductRepository.GetProductsCountFromDB(searchTerm);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}

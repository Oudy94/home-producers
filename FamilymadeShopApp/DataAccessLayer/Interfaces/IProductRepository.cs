using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsyncDAL(Product product);
        Product GetProductByIdDAL(int id);
        Task<int> GetProductsCountAsyncDAL(string filterName, Category? filterCategory);
        Task<List<Product>> GetAllProductsAsyncDAL(int pageNumber, int pageSize, string filterName, Category? filterCategory);
        Task<List<string>> GetProductsNamesAsyncDAL();
        Task<bool> UpdateProductsAsyncDAL(List<Product> products);
        Task RemoveProductByIdAsyncDAL(int id);
    }
}

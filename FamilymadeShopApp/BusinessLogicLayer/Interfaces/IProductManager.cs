using ModelLayer.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProductManager
    {
        Task AddProductAsync(Product product);
        Product GetProductById(int id);
        Task<List<Product>> GetAllProductsAsync(int pageNumber, int pageSize, string filterName, Category? filterCategory);
        Task<int> GetProductsCountAsync(string filterName, Category? filterCategory);
        Task<bool> UpdateProductsAsync(List<Product> products);
        Task<List<string>> GetProductsNamesAsync();
    }
}

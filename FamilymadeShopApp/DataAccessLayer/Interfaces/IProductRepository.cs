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
        Product GetProductByIdFromDB(int id);
        List<Product> GetProductsFromDB(string filterName = null, int pageNumber = 1);
		Task<int> GetProductsCountDBAsync(string filterName, Category? filterCategory);
    }
}

using ModelLayer.Models;
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
        List<Product> GetProductsFromDB(string searchTerm = null, int pageNumber = 1);
        int GetProductsCountFromDB(string searchTerm);
    }
}

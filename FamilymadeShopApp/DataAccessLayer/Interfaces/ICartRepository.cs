using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICartRepository
    {
        void AddCartDAL(Cart cart);
        void AddProductToCartDAL(int productId, int quantity, int customerId);
    }
}

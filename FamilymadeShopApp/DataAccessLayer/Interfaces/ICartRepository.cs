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
        void AddCartToDB(Cart cart);
        void AddProductToCartInDB(int productId, int quantity, int customerId);
    }
}

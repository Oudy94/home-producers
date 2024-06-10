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
        void AddProductToCartDAL(int customerId, int productId, int quantity);
        void RemoveCartByCustomerIdDAL(int customerId);
        Cart GetCartByCustomerIdDAL(int customerId);
        void UpdateProductQuantityInCartDAL(int customerId, int productId, int newQuantity);
    }
}

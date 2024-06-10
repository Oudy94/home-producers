using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICartManager
    {
        void AddCart(Cart cart);
        void AddProductToCart(int customerId, int productId, int quantity);
        void RemoveCartByCustomerId(int customerId);
        Cart GetCartByCustomerId(int customerId);
        void UpdateProductQuantityInCart(int customerId, int productId, int newQuantity);
    }
}

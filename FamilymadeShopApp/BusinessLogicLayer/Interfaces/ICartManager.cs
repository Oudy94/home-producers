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
        void AddProductToCart(int productId, int quantity, int customerId);
    }
}

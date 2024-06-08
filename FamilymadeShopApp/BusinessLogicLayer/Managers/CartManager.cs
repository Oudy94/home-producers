using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Managers
{
	public class CartManager: ICartManager
    {
		private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public void AddCart(Cart cart)
		{
			try
			{
                _cartRepository.AddCartDAL(cart);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public void AddProductToCart(int productId, int quantity, int customerId)
		{
            try
            {
                _cartRepository.AddProductToCartDAL(productId, quantity, customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
	}
}

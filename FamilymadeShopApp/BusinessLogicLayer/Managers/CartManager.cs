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

		public void AddProductToCart(int customerId, int productId, int quantity)
		{
            try
            {
                _cartRepository.AddProductToCartDAL(customerId, productId, quantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

		public void RemoveCartByCustomerId(int customerId)
		{
            try
            {
                _cartRepository.RemoveCartByCustomerIdDAL(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        public Cart GetCartByCustomerId(int customerId)
		{
            try
            {
                return _cartRepository.GetCartByCustomerIdDAL(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void UpdateProductQuantityInCart(int customerId, int productId, int newQuantity)
        {
            try
            {
                _cartRepository.UpdateProductQuantityInCartDAL(customerId, productId, newQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

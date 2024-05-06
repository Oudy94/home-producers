using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Models;

namespace BusinessLogicLayer.Managers
{
	public class CartManager : IManager<Cart>
	{
		private readonly CartRepository _CartRepository;

        public CartManager()
        {
            this._CartRepository = new CartRepository();
        }

        public void Add(Cart cart)
		{
			try
			{
                _CartRepository.AddCartToDB(cart);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public Cart Get(int id)
		{
			throw new NotImplementedException();
		}

		public List<Cart> GetAll()
		{
			throw new NotImplementedException();
		}

		public void AddProductToCart(int prodcutId, int quantity, int customerId)
		{
            try
            {
                _CartRepository.AddProductToCartInDB(prodcutId, quantity, customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
	}
}

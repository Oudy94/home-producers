using SharedLibrary.Helpers;
using SharedLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
	namespace SharedLibrary.Models
	{
		public class CartManager : IManager<Cart>
		{
			private readonly DatabaseHelper _dbHelper;

			public void Add(Cart cart)
			{
				try
				{
					_dbHelper.AddCartToDB(cart);
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
                    _dbHelper.AddProductToCartInDB(prodcutId, quantity, customerId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
		}
	}
}

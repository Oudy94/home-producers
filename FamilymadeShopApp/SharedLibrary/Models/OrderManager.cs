using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.Interfaces;

namespace SharedLibrary.Models
{
    public class OrderManager: IManager<Order>
    {
        public void Add(Order order)
        {
			throw new NotImplementedException();
		}

        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

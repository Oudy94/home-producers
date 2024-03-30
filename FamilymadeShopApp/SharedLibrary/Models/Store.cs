using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Store
    {
        public UserManager UserManager { get; set; }
        public ProductManager ProductManager { get; set; }
        public OrderManager OrderManager { get; set; }

        public Store(UserManager userManager, ProductManager productManager, OrderManager orderManager)
        {
            UserManager = userManager;
            ProductManager = productManager;
            OrderManager = orderManager;
        }
    }
}

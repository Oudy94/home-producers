using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Customer : User
    {
		public Cart Cart { get; set; }
		public string ShippingAddress { get; set; } = string.Empty;
		//public List<Order> Orders { get; set; }
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
	public class Cart
	{
		public int Id { get; }
		public Customer Customer { get; set; }
		public List<CartProduct> CartProducts { get; set; }
	}
}

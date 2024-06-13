using BusinessLogicLayer.Interfaces.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Discount
{
    public class PercentageDiscountGeneral : IGeneralDiscount
    {
        public decimal GetGeneralDiscount(decimal price)
        {
            decimal discountPercentage = 0.05m;
            return price * discountPercentage;
        }
    }

}

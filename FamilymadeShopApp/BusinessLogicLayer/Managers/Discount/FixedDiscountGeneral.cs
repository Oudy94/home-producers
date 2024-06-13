using BusinessLogicLayer.Interfaces.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Discount
{
    public class FixedDiscountGeneral : IGeneralDiscount
    {

        public decimal GetGeneralDiscount(decimal price)
        {
            return 5.0m;
        }
    }
}

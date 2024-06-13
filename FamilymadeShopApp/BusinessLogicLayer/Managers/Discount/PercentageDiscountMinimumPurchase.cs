using BusinessLogicLayer.Interfaces.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Discount
{
    public class PercentageDiscountMinimumPurchase : IMinimumPurchaseDiscount
    {
        private decimal _minimumPurchaseAmount;

        public PercentageDiscountMinimumPurchase(decimal minimumPurchaseAmount)
        {
            _minimumPurchaseAmount = minimumPurchaseAmount;
        }

        public decimal GetMinimumPurchaseDiscount(decimal price)
        {
            if (price >= _minimumPurchaseAmount)
            {
                decimal discountPercentage = 0.10m;
                return price * discountPercentage;
            }

            return 0;
        }
    }
}

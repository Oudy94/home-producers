using BusinessLogicLayer.Interfaces.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Discount
{
    public class FixedDiscountMinimalPurchase : IMinimumPurchaseDiscount
    {
        private decimal _minimumPurchaseAmount;

        public FixedDiscountMinimalPurchase(decimal minimumPurchaseAmount)
        {
            _minimumPurchaseAmount = minimumPurchaseAmount;
        }

        public decimal GetMinimumPurchaseDiscount(decimal price)
        {
            if (price >= _minimumPurchaseAmount)
            {
                return 20.0m;
            }

            return 0;
        }
    }
}

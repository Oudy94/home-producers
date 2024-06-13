using BusinessLogicLayer.Interfaces.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Discount.Factory
{
    public class FixedDiscountFactory : IDiscountFactory
    {
        public IGeneralDiscount CreateGeneralDiscount()
        {
            return new FixedDiscountGeneral();
        }

        public IMinimumPurchaseDiscount CreateMinimumPurchaseDiscount(decimal minimumPurchaseAmount)
        {
            return new FixedDiscountMinimalPurchase(minimumPurchaseAmount);
        }
    }
}

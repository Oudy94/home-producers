using BusinessLogicLayer.Interfaces.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Discount.Factory
{
    public class PercentageDiscountFactory : IDiscountFactory
    {
        public IGeneralDiscount CreateGeneralDiscount()
        {
            return new PercentageDiscountGeneral();
        }

        public IMinimumPurchaseDiscount CreateMinimumPurchaseDiscount(decimal minimumPurchaseAmount)
        {
            return new PercentageDiscountMinimumPurchase(minimumPurchaseAmount);
        }
    }

}

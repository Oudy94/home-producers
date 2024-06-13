using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Discount
{
    public interface IDiscountFactory
    {
        IGeneralDiscount CreateGeneralDiscount();
        IMinimumPurchaseDiscount CreateMinimumPurchaseDiscount(decimal minimumPurchaseAmount);
    }

}

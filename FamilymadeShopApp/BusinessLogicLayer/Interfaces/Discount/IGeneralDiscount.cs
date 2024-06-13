using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Discount
{
    public interface IGeneralDiscount
    {
        decimal GetGeneralDiscount(decimal price);
    }
}

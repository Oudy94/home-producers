using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Payment
{
    public class CreditCardPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(decimal amount)
        {
            return true;
        }
    }
}

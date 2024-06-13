using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPaymentStrategy
    {
        PaymentResult ProcessPayment(decimal amount);
        decimal GetTransactionFees(decimal amount);
    }
}

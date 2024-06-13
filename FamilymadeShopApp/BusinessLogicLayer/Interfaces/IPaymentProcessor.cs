using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPaymentProcessor
    {
        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy);
        public PaymentResult ProcessPayment(decimal totalAmount);
        public decimal GetTransactionFee(decimal totalAmount);
    }
}

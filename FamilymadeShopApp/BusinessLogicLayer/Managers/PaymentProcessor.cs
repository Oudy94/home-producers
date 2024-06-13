using BusinessLogicLayer.Interfaces;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class PaymentProcessor
    {
        private IPaymentStrategy _paymentStrategy;

        public PaymentProcessor(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public PaymentResult ProcessPayment(decimal totalAmount)
        {
            return _paymentStrategy.ProcessPayment(totalAmount);
        }

        public decimal GetTransactionFee(decimal totalAmount)
        {
            return _paymentStrategy.GetTransactionFees(totalAmount);
        }
    }                          
}

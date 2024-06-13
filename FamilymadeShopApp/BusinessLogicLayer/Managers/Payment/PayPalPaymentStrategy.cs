using BusinessLogicLayer.Interfaces;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers.Payment
{
    public class PayPalPaymentStrategy : IPaymentStrategy
    {
        public PaymentResult ProcessPayment(decimal amount)
        {
            if (amount <= 0)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = "Invalid amount."
                };
            }

            if (false) //imitate Insufficient amount
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = "Insufficient amount in the PayPal."
                };
            }

            return new PaymentResult
            {
                Success = true,
                Message = $"PayPal payment of €{amount} processed successfully."
            };
        }

        public decimal GetTransactionFees(decimal amount)
        {
            return amount * 0.03m;
        }
    }
}

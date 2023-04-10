using ProvaPub.Generic;
using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class PaymentFactory
    {
        public static IPaymentService GetService(string paymentMethod)
        {
            switch(paymentMethod)
            {
                case PaymentService.CreditCard:
                    return new CreditCardPaymentService();
                case PaymentService.Pix:
                    return new PixPaymentService();
                case PaymentService.PayPal:
                    return new PayPalPaymentService();
                default:
                    throw new ArgumentException($"Payment Method not found for { paymentMethod  }");
            }
        }
    }
}

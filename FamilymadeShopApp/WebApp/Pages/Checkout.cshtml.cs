using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces.Discount;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Managers.Discount.Factory;
using BusinessLogicLayer.Managers.Payment;
using DataAccessLayer.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public UserManager UserManager { get; set; }
        public ProductManager ProductManager { get; set; }
        public OrderManager OrderManager { get; set; }
        public PaymentProcessor PaymentProcessor { get; set; }
        public List<CartProduct> CartItems { get; set; }
        public IGeneralDiscount GeneralDiscount { get; set; }
        public IMinimumPurchaseDiscount MinimumPurchaseDiscount { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Address Address { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please select a payment method.")]
        public string SelectedPaymentMethod { get; set; }

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("CartItems"))
            {
                TempData["MessageDanger"] = "Your cart is empty!";
                return RedirectToPage("/Index");
            }

            CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

            if (CartItems == null || CartItems.Count == 0)
            {
                TempData["MessageDanger"] = "Your cart is empty!";
                return RedirectToPage("/Index");
            }

            //in case the product name or price is changed we update the information
            ProductManager = new ProductManager(new ProductRepository());
            foreach (CartProduct cartItem in CartItems)
            {
                Product product = ProductManager.GetProductById(cartItem.ProductId);
                cartItem.Name = product.Name;
                cartItem.Price = product.Price;
            }
            HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(CartItems));

            //populate customer form fields
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null)
            {
                if (int.TryParse(userIdClaim.Value, out int userId))
                {
                    UserManager = new UserManager(new UserRepository());
                    Customer = UserManager.GetCustomerById(userId);
                }
            }

            Address = new Address();

            //Discount
            IDiscountFactory percentageDiscountFactory = new PercentageDiscountFactory();
            GeneralDiscount = percentageDiscountFactory.CreateGeneralDiscount();
            MinimumPurchaseDiscount = percentageDiscountFactory.CreateMinimumPurchaseDiscount(50);

            return Page();
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            IPaymentStrategy paymentStrategy;
            switch (SelectedPaymentMethod)
            {
                case "creditcard":
                    paymentStrategy = new CreditCardPaymentStrategy();
                    break;
                case "paypal":
                    paymentStrategy = new PayPalPaymentStrategy();
                    break;
                default:
                    ModelState.AddModelError("", "Invalid payment method selected.");
                    return Page();
            }

            PaymentProcessor = new PaymentProcessor(paymentStrategy);

            IDiscountFactory percentageDiscountFactory = new PercentageDiscountFactory();
            GeneralDiscount = percentageDiscountFactory.CreateGeneralDiscount();
            MinimumPurchaseDiscount = percentageDiscountFactory.CreateMinimumPurchaseDiscount(50);

            decimal totalProductsPrice = CalculateTotalProductsPrice();
            decimal shippingAmount = CalculateShippingAmount();
            decimal transactionFee = CalculateTransactionFee(totalProductsPrice);
            decimal discountPrice = CalculateDiscountPrice(totalProductsPrice);
            decimal totalAmount = Math.Round(totalProductsPrice + shippingAmount + transactionFee - discountPrice, 2);
            PaymentResult paymentResult = PaymentProcessor.ProcessPayment(totalAmount);

            if (!paymentResult.Success)
            {
                TempData["MessageDanger"] = paymentResult.Message;
                return RedirectToPage("/Index");
            }

            ProductManager = new ProductManager(new ProductRepository());
            CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

            List<OrderProduct> orderProducts = new List<OrderProduct>();

            foreach (CartProduct cartItem in CartItems)
            {
                Product product = ProductManager.GetProductById(cartItem.ProductId);
                orderProducts.Add(new OrderProduct(product, cartItem.Quantity, product.Price));
            }

            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null)
            {
                if (int.TryParse(userIdClaim.Value, out int userId))
                {
                    UserManager = new UserManager(new UserRepository());
                    OrderManager = new OrderManager(new OrderRepository());

                    Customer = UserManager.GetCustomerById(userId);
                    decimal shippingPrice = CalculateShippingAmount();
                    Order order = new Order(Customer.Id, SharedLayer.Enums.OrderStatus.Pending, DateTime.Now, orderProducts, shippingPrice, Address, SelectedPaymentMethod, PaymentProcessor.GetTransactionFee(totalAmount));

                    OrderManager.AddOrder(order);

                    CartManager cartManager = new CartManager(new CartRepository());
                    cartManager.RemoveCartByCustomerId(userId);
                }
            }

            Response.Cookies.Delete("CartItems");
            TempData["MessageSuccess"] = paymentResult.Message;

            return RedirectToPage("/Index");
        }

        private decimal CalculateTotalProductsPrice()
        {
            decimal totalAmount = 0;
            CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

            foreach (var cartItem in CartItems)
            {
                totalAmount += cartItem.Quantity * cartItem.Price;
            }
            return totalAmount;
        }

        private decimal CalculateShippingAmount()
        {
            //if (VIP)
            //    return 0;
            return 10;
        }

        private decimal CalculateDiscountPrice(decimal totalProductPrice)
        {
            return GeneralDiscount.GetGeneralDiscount(totalProductPrice) + MinimumPurchaseDiscount.GetMinimumPurchaseDiscount(totalProductPrice);
        }
        
        private decimal CalculateTransactionFee(decimal totalProductPrice)
        {
            return PaymentProcessor.GetTransactionFee(totalProductPrice);
        }
    }
}

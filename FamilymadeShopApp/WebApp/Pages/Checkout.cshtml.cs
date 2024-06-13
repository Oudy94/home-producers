using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces.Discount;
using BusinessLogicLayer.Managers.Payment;
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
        private readonly IUserManager _userManager;
        private readonly IProductManager _productManager;
        private readonly IOrderManager _orderManager;
        private readonly ICartManager _cartManager;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IDiscountFactory _discountFactory;

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

        public CheckoutModel(
            IUserManager userManager,
            IProductManager productManager,
            IOrderManager orderManager,
            ICartManager cartManager,
            IPaymentProcessor paymentProcessor,
            IDiscountFactory discountFactory)
        {
            _userManager = userManager;
            _productManager = productManager;
            _orderManager = orderManager;
            _cartManager = cartManager;
            _paymentProcessor = paymentProcessor;
            _discountFactory = discountFactory;
        }

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("CartItems"))
            {
                return ShowErrorAndRedirect("Your cart is empty!");
            }

            CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

            if (CartItems == null || CartItems.Count == 0)
            {
                return ShowErrorAndRedirect("Your cart is empty!");
            }

            if (!UpdateCartItems() || !LoadCustomerData())
            {
                return ShowErrorAndRedirect("Failed checkout. Please try again later.");
            }

            Address = new Address();
            InitializeDiscounts();

            return Page();
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("Customer.Cart");
            ModelState.Remove("Customer.Password");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var paymentStrategy = GetPaymentStrategy();
            if (paymentStrategy == null)
            {
                ModelState.AddModelError("", "Invalid payment method selected.");
                return Page();
            }

            if (!SetPaymentStrategy(paymentStrategy))
            {
                return ShowErrorAndRedirect("Failed checkout. Please try again later.");
            }

            InitializeDiscounts();

            CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

            decimal totalProductsPrice = CalculateTotalProductsPrice();
            decimal shippingAmount = CalculateShippingAmount();
            decimal transactionFee = CalculateTransactionFee(totalProductsPrice);
            decimal generalDiscount = CalculateGeneralDiscountPrice(totalProductsPrice);
            decimal minimumPurchaseDiscount = CalculateMinimumPurchaseDiscountPrice(totalProductsPrice);
            decimal discountPrice = generalDiscount + minimumPurchaseDiscount;
            decimal totalAmount = Math.Round(totalProductsPrice + shippingAmount + transactionFee - discountPrice, 2);

            PaymentResult paymentResult = ProcessPayment(totalAmount);
            if (!paymentResult.Success)
            {
                return ShowErrorAndRedirect(paymentResult.Message);
            }

            if (!ProcessOrder(shippingAmount, transactionFee, generalDiscount, minimumPurchaseDiscount))
            {
                return ShowErrorAndRedirect("Failed to process order. Please try again later.");
            }

            Response.Cookies.Delete("CartItems");
            TempData["MessageSuccess"] = paymentResult.Message;

            return RedirectToPage("/Index");
        }

        private bool UpdateCartItems()
        {
            try
            {

                foreach (var cartItem in CartItems)
                {
                    var product = _productManager.GetProductById(cartItem.ProductId);
                    cartItem.Name = product.Name;
                    cartItem.Price = product.Price;
                }

                HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(CartItems));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool LoadCustomerData()
        {
            try
            {
                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    Customer = _userManager.GetCustomerById(userId);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void InitializeDiscounts()
        {
            GeneralDiscount = _discountFactory.CreateGeneralDiscount();
            MinimumPurchaseDiscount = _discountFactory.CreateMinimumPurchaseDiscount(50);
        }

        private IPaymentStrategy GetPaymentStrategy()
        {
            switch (SelectedPaymentMethod)
            {
                case "creditcard":
                    return new CreditCardPaymentStrategy();
                case "paypal":
                    return new PayPalPaymentStrategy();
                default:
                    return null;
            }
        }

        private bool SetPaymentStrategy(IPaymentStrategy paymentStrategy)
        {
            try
            {
                _paymentProcessor.SetPaymentStrategy(paymentStrategy);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private PaymentResult ProcessPayment(decimal totalAmount)
        {
            try
            {
                return _paymentProcessor.ProcessPayment(totalAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PaymentResult { Success = false, Message = "Failed checkout. Please try again later." };
            }
        }

        private bool ProcessOrder(decimal shippingAmount, decimal transactionFee, decimal generalDiscount, decimal minimumPurchaseDiscount)
        {
            try
            {
                var orderProducts = CreateOrderProducts();

                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    Customer = _userManager.GetCustomerById(userId);
                    var shippingPrice = CalculateShippingAmount();
                    var order = new Order(Customer.Id, SharedLayer.Enums.OrderStatus.Pending, DateTime.Now, orderProducts, shippingAmount, Address, SelectedPaymentMethod, transactionFee);

                    _orderManager.AddOrder(order);
                    _cartManager.RemoveCartByCustomerId(userId);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private List<OrderProduct> CreateOrderProducts()
        {
            var orderProducts = new List<OrderProduct>();

            foreach (var cartItem in CartItems)
            {
                try
                {
                    var product = _productManager.GetProductById(cartItem.ProductId);
                    orderProducts.Add(new OrderProduct(product, cartItem.Quantity, product.Price));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("Failed to create order products.", ex);
                }
            }

            return orderProducts;
        }

        private decimal CalculateTotalProductsPrice()
        {
            return CartItems.Sum(cartItem => cartItem.Quantity * cartItem.Price);
        }

        private decimal CalculateShippingAmount()
        {
            //if (isVIP)
            //    return 0;
            return 10;
        }

        private decimal CalculateGeneralDiscountPrice(decimal totalProductPrice)
        {
            try
            {
                return Math.Round(GeneralDiscount.GetGeneralDiscount(totalProductPrice), 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to calculate discount price.", ex);
            }
        }

        private decimal CalculateMinimumPurchaseDiscountPrice(decimal totalProductPrice)
        {
            try
            {
                return Math.Round(MinimumPurchaseDiscount.GetMinimumPurchaseDiscount(totalProductPrice), 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to calculate discount price.", ex);
            }
        }

        private decimal CalculateTransactionFee(decimal totalProductPrice)
        {
            try
            {
                return Math.Round(_paymentProcessor.GetTransactionFee(totalProductPrice), 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to calculate transaction fee.", ex);
            }
        }

        private IActionResult ShowErrorAndRedirect(string message)
        {
            TempData["MessageError"] = message;
            return RedirectToPage("/Index");
        }
    }

}

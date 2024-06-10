using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.DataAccess;
using BusinessLogicLayer.Managers.Payment;
using BusinessLogicLayer.Interfaces;

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

        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Address Address { get; set; }
        [BindProperty]
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

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(SelectedPaymentMethod))
            {
                ModelState.AddModelError("", "Payment method is required.");
                return Page();
            }

            decimal totalAmount = CalculateTotalAmount() + CalculateShippingAmount();

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

            bool paymentSuccessful = PaymentProcessor.ProcessPayment(totalAmount);

            if (!paymentSuccessful)
            {
                TempData["MessageDanger"] = "Payment failed. Please try again.";
                return RedirectToPage("/Index");
            }

            ProcessOrder();

            return RedirectToPage("/Index");
        }

        private decimal CalculateTotalAmount()
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

        private void ProcessOrder()
        {
            ProductManager = new ProductManager(new ProductRepository());
            CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

            List<OrderProduct> orderProducts = new List<OrderProduct>();

            foreach (CartProduct cartItem in CartItems)
            {
                Product product = ProductManager.GetProductById(cartItem.ProductId);
                orderProducts.Add(new OrderProduct(product, cartItem.Quantity, product.Price));
            }

            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null)
                {
                    if (int.TryParse(userIdClaim.Value, out int userId))
                    {
                        UserManager = new UserManager(new UserRepository());
                        OrderManager = new OrderManager(new OrderRepository());

                        Customer = UserManager.GetCustomerById(userId);
                        decimal shippingPrice = CalculateShippingAmount();
                        StringBuilder fullAddress = new StringBuilder();
                        fullAddress.Append(Address.AddressLine);
                        fullAddress.Append(", ");
                        fullAddress.Append(Address.PostalCode);
                        fullAddress.Append(", ");
                        fullAddress.Append(Address.City);
                        fullAddress.Append(", ");
                        fullAddress.Append(Address.Country);
                        Order order = new Order(Customer.Id, SharedLayer.Enums.OrderStatus.Pending, DateTime.Now, orderProducts, shippingPrice, fullAddress.ToString(), SelectedPaymentMethod);

                        OrderManager.AddOrder(order);
                    }
                }
            }

            Response.Cookies.Delete("CartItems");
            TempData["MessageSuccess"] = "Your order was created successfully.";
        }
    }

    public class Address
    {
        private string _postalCode;

        [Required(ErrorMessage = "Address  is required")]
        [Display(Name = "Address")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression(@"^\d{4}\s?[A-Za-z]{2}$", ErrorMessage = "Invalid Zip Code")]
        [Display(Name = "Postal Code")]
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _postalCode = value;
                }
                else
                {
                    string trimmedValue = value.Trim();

                    if (trimmedValue.Length == 6 && char.IsDigit(trimmedValue[4]) && char.IsLetter(trimmedValue[5]))
                    {
                        _postalCode = trimmedValue.Insert(4, " ");
                    }
                    else
                    {
                        _postalCode = trimmedValue;
                    }

                    _postalCode = _postalCode.Substring(0, 4) + _postalCode.Substring(4).ToUpper();
                }
            }
        }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = "Netherlands";
    }
}

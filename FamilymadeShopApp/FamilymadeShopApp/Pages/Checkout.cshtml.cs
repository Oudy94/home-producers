using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Pages
{
    public class CheckoutModel : PageModel
    {
        public UserManager UserManager { get; set; }
        public ProductManager ProductManager { get; set; }
        public OrderManager OrderManager { get; set; }

        public List<CartProduct> CartItems { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Address Address { get; set; }

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

            //TODO: accept order from guest
            if (!User.Identity.IsAuthenticated)
            {
                TempData["MessageDanger"] = "You need to be logged in to make an order.";
                return RedirectToPage("/Index");
            }

            ProductManager = new ProductManager();

            foreach (CartProduct cartItem in CartItems)
            {
                Product product = ProductManager.Get(cartItem.ProductId);

                cartItem.Name = product.Name;
                cartItem.Price = product.Price;
                cartItem.ImageUrl = product.Images[0];
            }

            HttpContext.Response.Cookies.Append("CartItems", JsonConvert.SerializeObject(CartItems));

            if (User.Identity.IsAuthenticated)
            {
                UserManager = new UserManager();

                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null)
                {
                    if (int.TryParse(userIdClaim.Value, out int userId))
                    {
                        UserManager = new UserManager();
                        Customer = UserManager.Get(userId);
                    }
                }
            }

            Address = new Address();

            return Page();
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            ProductManager = new ProductManager();
            CartItems = JsonConvert.DeserializeObject<List<CartProduct>>(Request.Cookies["CartItems"]);

            List<OrderProduct> orderProducts = new List<OrderProduct>();

            foreach (CartProduct cartItem in CartItems)
            {
                Product product = ProductManager.Get(cartItem.ProductId);
                orderProducts.Add(new OrderProduct(product, cartItem.Quantity, product.Price));
            }

            if (User.Identity.IsAuthenticated)
            {

                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null)
                {
                    if (int.TryParse(userIdClaim.Value, out int userId))
                    {
                        UserManager = new UserManager();
                        OrderManager = new OrderManager();

                        Customer = UserManager.Get(userId);
                        //decimal shippingPrice = isVip ? 0 : 10;
                        decimal shippingPrice = 10;
                        StringBuilder fullAddress = new StringBuilder();
                        fullAddress.Append(Address.AddressLine);
                        fullAddress.Append(", ");
                        fullAddress.Append(Address.PostalCode);
                        fullAddress.Append(", ");
                        fullAddress.Append(Address.City);
                        fullAddress.Append(", ");
                        fullAddress.Append(Address.Country);
                        Order order = new Order(Customer.Id, SharedLayer.Enums.OrderStatusEnum.Pending, DateTime.Now, orderProducts, shippingPrice, fullAddress.ToString());

                        OrderManager.Add(order);
                    }
                }
            }

            Response.Cookies.Delete("CartItems");
            TempData["MessageSuccess"] = "Your order created successfully.";
            return RedirectToPage("/Index");
        }
    }

    public class Address
    {
        [Required(ErrorMessage = "Address  is required")]
        [Display(Name = "Address")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression(@"^\d{4}\s?[A-Za-z]{2}$", ErrorMessage = "Invalid Zip Code")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = "Netherlands";
    }
}

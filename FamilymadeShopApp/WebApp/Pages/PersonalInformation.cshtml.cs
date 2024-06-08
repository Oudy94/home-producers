using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelLayer.Models;
using BusinessLogicLayer.Managers;
using DataAccessLayer.DataAccess;

namespace WebApp.Pages
{
    [Authorize]
    public class PersonalInformationModel : PageModel
    {
        UserManager UserManager { get; set; }
        OrderManager OrderManager { get; set; }
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }

        public void OnGet()
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null)
            {
                if (int.TryParse(userIdClaim.Value, out int userId))
                {
                    UserManager = new UserManager(new UserRepository());
                    OrderManager = new OrderManager(new OrderRepository());
                    Customer = UserManager.GetCustomerById(userId);

                    Orders = OrderManager.GetOrdersByUserId(userId);
                }
            }
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedLibrary.Models;

namespace FamilymadeShopApp.Pages
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
            UserManager = new UserManager();

            var userIdClaim = User.FindFirst("id");
            if (userIdClaim != null)
            {
                if (int.TryParse(userIdClaim.Value, out int userId))
                {
                    UserManager = new UserManager();
                    OrderManager = new OrderManager();
                    Customer = UserManager.Get(userId);

                    Orders = OrderManager.GetOrdersByUserId(userId);
                }
            }
        }

    }
}

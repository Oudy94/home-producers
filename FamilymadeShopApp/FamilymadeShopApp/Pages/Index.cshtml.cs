using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (TempData.ContainsKey("MessageDanger"))
            {
                string message = TempData["MessageDanger"].ToString();
                ViewData["MessageDanger"] = message;
            }
            else if (TempData.ContainsKey("MessageSuccess"))
            {
                string message = TempData["MessageSuccess"].ToString();
                ViewData["MessageSuccess"] = message;
            }
        }
    }
}

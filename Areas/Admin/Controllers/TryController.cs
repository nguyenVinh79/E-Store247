using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Project.Areas.Admin.Controllers
{
    public class TryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
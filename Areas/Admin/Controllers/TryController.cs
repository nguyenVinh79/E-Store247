using Microsoft.AspNetCore.Mvc;

namespace ShopBanHang.Areas.Admin.Controllers
{
    public class TryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
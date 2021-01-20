using Microsoft.AspNetCore.Mvc;

namespace ShopBanHang.Controllers
{
    public class BlogCateController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
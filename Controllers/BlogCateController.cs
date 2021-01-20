using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Project.Controllers
{
    public class BlogCateController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
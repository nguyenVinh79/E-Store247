using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerce.Project.Models;
using System.Threading.Tasks;

namespace ECommerce.Project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly DataShopContext _db;

        public FooterViewComponent(DataShopContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _db.Settings.ToListAsync());
        }
    }
}
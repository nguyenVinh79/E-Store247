using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.ViewComponents
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataShopContext _db;


        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration, DataShopContext dbcontext)
        {
            _logger = logger;
            _configuration = Configuration;
            _db = dbcontext;
        }

        public IActionResult Index()
        {

            //var myKeyValue = _configuration["MyKey"];
            //var title = _configuration["Position:Title"];
            //var name = _configuration["Position:Name"];
            //var defaultLogLevel = _configuration["Logging:LogLevel:Default"];
            //int pagesize = int.Parse(_configuration["pagesize"]);

            var Slider = _db.Banners.Where(x => x.IsShow == true && x.Type == "Slider").ToList();
            var Categories = _db.Categories.Where(x => x.IsDeleted != true).ToList();

            ViewBag.Slider = Slider;
            ViewBag.Categories = Categories;

            #region GetData Product

            var lstproduct = _db.Products.Where(x => x.IsDeleted != true).ToList();

            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

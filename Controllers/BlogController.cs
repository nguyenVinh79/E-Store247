﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShopBanHang.Controllers
{
    public class BlogController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

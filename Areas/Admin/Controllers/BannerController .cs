using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using ShopBanHang.Areas.Admin.Models;
using ShopBanHang.Helper;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly IMapper _mapper;
        private DataShopContext db;
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _env;
        public BannerController(IMapper mapper, IConfiguration configuration, DataShopContext dbcontext, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            db = dbcontext;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            var BannerList = new List<Banner>();
            try
            {
                BannerList = db.Banners.Where(x => x.IsShow == true).ToList();
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = "Error in loanding banner list";
            }
            return View(BannerList);
        }
        public IActionResult Create(int id = 0)
        {
            var model = new BannerModel();


            if (id > 0)
            {

                var BannerData = db.Banners.Where(x => x.ID == id).FirstOrDefault();
                if (BannerData == null)
                {
                    return RedirectToAction("Index");
                }

                model = _mapper.Map<BannerModel>(BannerData);

            }



            return View(model);
        }
        [HttpPost]
        public IActionResult Create(BannerModel model, IFormFile image)
        {

            try
            {
                if (ModelState.IsValid)
                {


                    if (model.ID == 0)
                    {

                        #region For Create


                        var banner = _mapper.Map<Banner>(model);
                        if (image != null)
                        {
                            string urlImage = MyTool.UploadImage(image, "wwwroot", "Image", "Sliders");
                            if (!string.IsNullOrEmpty(urlImage))
                            {
                                banner.ImagePath = urlImage;
                            }
                        }

                        db.Banners.Add(banner);
                        db.SaveChanges();

                        #endregion

                    }
                    else
                    {

                        #region for edit

                        var bannerEdit = _mapper.Map<Banner>(model);

                        if (image != null)
                        {
                            string urlImage = MyTool.UploadImage(image, "wwwroot", "Image", "Sliders");
                            if (!string.IsNullOrEmpty(urlImage))
                            {
                                bannerEdit.ImagePath = urlImage;
                            }
                        }

                        db.Update(bannerEdit);
                        db.SaveChanges();

                        #endregion

                    }

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = ex.Message;

                return View(model);
            }

            return View(model);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var banner = db.Banners.Find(Convert.ToInt64(id));
            if (banner != null)
            {

                db.Banners.Remove(banner);
                try
                {
                    db.SaveChanges();
                    return Json(new
                    {
                        status = true
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        status = false,
                        message = ex.Message
                    });
                }
            }

            return Json(new
            {
                status = false,
                message = "Delete failed!"
            });
        }
       
        
    }
}

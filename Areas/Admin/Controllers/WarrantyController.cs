using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopBanHang.Areas.Admin.Models;
using ShopBanHang.Models;

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WarrantyController : Controller
    {
        private readonly IMapper _mapper;
        private DataShopContext db;
        private readonly IConfiguration Configuration;

        public WarrantyController(IMapper mapper, IConfiguration configuration, DataShopContext dbcontext)
        {
            this._mapper = mapper;
            db = dbcontext;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            var warrantyList = new List<CT_WarrantyTime>();
            try
            {
                warrantyList = db.CT_WarrantyTimes.ToList();
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = "Error in loanding warranty list";
            }
            return View(warrantyList);
        }
        public IActionResult Create(int id = 0)
        {
            var model = new WarrantyModel();

            if (id > 0)
            {
                var warrantyData = db.CT_WarrantyTimes.Where(x => x.ID == id).FirstOrDefault();
                model = _mapper.Map(warrantyData, model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(WarrantyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        #region For Create

                        var warranty = _mapper.Map<CT_WarrantyTime>(model);
                        db.CT_WarrantyTimes.Add(warranty);
                        db.SaveChanges();

                        #endregion For Create
                    }
                    else
                    {
                        #region for edit

                        var warrantyEdit = _mapper.Map<CT_WarrantyTime>(model);

                        db.Update(warrantyEdit);
                        db.SaveChanges();

                        #endregion for edit
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
            var warranty = db.CT_WarrantyTimes.Find(Convert.ToInt64(id));
            if (warranty != null)
            {
                var productsOwnWarranty = db.Products.Where(x => x.CT_WarrantyTimeID == id).ToList();
                if (productsOwnWarranty.Count > 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "One or more product has been used this warranty time!"
                    });
                }
                else
                {
                    db.CT_WarrantyTimes.Remove(warranty);
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
            }
            return Json(new
            {
                status = false,
                message = "Delete failed!"
            });
        }
    }
}

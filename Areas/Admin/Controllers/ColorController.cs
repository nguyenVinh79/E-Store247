using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ECommerce.Project.Areas.Admin.Models;
using ECommerce.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Project.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IMapper _mapper;
        private DataShopContext db;
        private readonly IConfiguration Configuration;

        public ColorController(IMapper mapper, IConfiguration configuration, DataShopContext dbcontext)
        {
            this._mapper = mapper;
            db = dbcontext;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            var colorList = new List<CT_Color>();
            try
            {
                colorList = db.CT_Colors.ToList();
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = "Error in loanding color list";
            }
            return View(colorList);
        }

        public IActionResult Create(int id = 0)
        {
            var model = new ColorModel();

            if (id > 0)
            {
                var colorData = db.CT_Colors.Where(x => x.ColorId == id).FirstOrDefault();
                model = _mapper.Map(colorData, model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ColorModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        #region For Create

                        var color = _mapper.Map<CT_Color>(model);
                        db.CT_Colors.Add(color);
                        db.SaveChanges();

                        #endregion For Create
                    }
                    else
                    {
                        #region for edit

                        var colorEdit = _mapper.Map<CT_Color>(model);

                        db.Update(colorEdit);
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
            var color = db.CT_Colors.Find(id);
            if (color != null)
            {
                var productsOwnColor = db.ProductColorSizes.Where(x => x.CodeColor == color.Code).ToList();
                if (productsOwnColor.Count > 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "One or more product has been used this color!"
                    });
                }
                else
                {
                    db.CT_Colors.Remove(color);
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
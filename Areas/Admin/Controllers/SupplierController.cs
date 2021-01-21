using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ECommerce.Project.Areas.Admin.Models;
using ECommerce.Project.Helper;
using ECommerce.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Project.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly IMapper _mapper;
        private DataShopContext db;
        private readonly IConfiguration Configuration;

        public SupplierController(IMapper mapper, IConfiguration configuration, DataShopContext dbcontext)
        {
            this._mapper = mapper;
            db = dbcontext;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            var supplierList = new List<Supplier>();
            try
            {
                supplierList = db.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = "Error in loanding category list";
            }
            return View(supplierList);
        }

        public IActionResult Create(int id = 0)
        {
            var model = new SupplierVM();

            if (id > 0)
            {
                var supplierData = db.Suppliers.Where(x => x.ID == id).FirstOrDefault();
                model = _mapper.Map(supplierData, model);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SupplierVM model, IFormFile Logo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ID == 0)
                    {
                        #region For Create

                        var supplier = _mapper.Map<Supplier>(model);
                        if (Logo != null)
                        {
                            string urlImage = MyTool.UploadImage(Logo, "wwwroot", "Image", "Suppliers");
                            if (!string.IsNullOrEmpty(urlImage))
                            {
                                supplier.Logo = urlImage;
                            }
                        }

                        db.Suppliers.Add(supplier);
                        db.SaveChanges();

                        #endregion For Create
                        TempData["StatusMessage"] = "Successfully created";
                    }
                    else
                    {
                        #region for edit

                        var supplierEdit = _mapper.Map<Supplier>(model);

                        if (Logo != null)
                        {
                            string urlImage = MyTool.UploadImage(Logo, "wwwroot", "Image", "Suppliers");
                            if (!string.IsNullOrEmpty(urlImage))
                            {
                                supplierEdit.Logo = urlImage;
                            }
                        }

                        db.Update(supplierEdit);
                        db.SaveChanges();

                        #endregion for edit
                        TempData["StatusMessage"] = "Successfully updated";
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
            var supplier = db.Suppliers.Find(id);
            if (supplier != null)
            {
                var productsOfSupplier = db.Products.Where(x => x.SupplierID == id).ToList();
                if (productsOfSupplier.Count > 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "One or more product has been used this supplier!"
                    });
                }
                else
                {
                    db.Suppliers.Remove(supplier);
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopBanHang.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Newtonsoft.Json;

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private DataShopContext db;
        private readonly IWebHostEnvironment _env;
        // requires using Microsoft.Extensions.Configuration;


        private readonly IConfiguration Configuration;


        public ProductController(IMapper mapper, IWebHostEnvironment env, IConfiguration configuration, DataShopContext dbcontext)
        {
            this._mapper = mapper;
            _env = env;
            db = dbcontext;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            var myKeyValue = Configuration["MyKey"];
            var title = Configuration["Position:Title"];
            var name = Configuration["Position:Name"];
            var defaultLogLevel = Configuration["Logging:LogLevel:Default"];





            var lstdata = new List<Product>();

            lstdata = db.Products.Where(x => x.IsDeleted != true).ToList();

            return View(lstdata);
        }

        public IActionResult Create(int id = 0)
        {

            var model = new ProductModel();
            ViewBag.ColorList = db.CT_Colors.Where(x => x.Active == true).ToList();            
            List<Category> lstData = db.Categories.ToList();
            model.DetailImageList = new List<Image>();

            if (id > 0)
            {
                ViewBag.PropertyList = db.ProductColorSizes.Where(x => x.ProductID == id).ToList();
                var dataProduct = db.Products.Where(x => x.ID == id && x.IsDeleted != true).FirstOrDefault();

                model = _mapper.Map<ProductModel>(dataProduct);
                model.DetailImageList = db.Images.Where(x => x.ReferenceId == id).ToList();
                
            }
            model.CategoryList = new List<SelectListItem>();

            var initItem = new SelectListItem();
            initItem.Value = "".ToString();
            initItem.Text = "-- Choose specified category of product--";
            model.CategoryList.Add(initItem);

            foreach (var item in lstData)
            {
                var tempItem = new SelectListItem();
                tempItem.Value = item.ID.ToString();
                tempItem.Text = item.CategoryName;

                model.CategoryList.Add(tempItem);
            }




            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductModel model, IFormFile Avatar, List<IFormFile> DetailImages, string PropertyJsonString)
        {

            try
            {
                string wwwPath = _env.WebRootPath;
                string contentPath = _env.ContentRootPath;

                string AvatarPath = Path.Combine(this._env.WebRootPath, "Image", "Products");
                string DetailImagePath = Path.Combine(this._env.WebRootPath, "Image", "Products", "Details");

                if (!Directory.Exists(DetailImagePath))
                {
                    Directory.CreateDirectory(DetailImagePath);

                }
                if (!Directory.Exists(AvatarPath))
                {
                    Directory.CreateDirectory(AvatarPath);

                }
                List<ProductColorSize> PropertyList = JsonConvert.DeserializeObject<List<ProductColorSize>>(PropertyJsonString);

                if (ModelState.IsValid)
                {
                    string fileNameAvata = "";
                    if (Avatar != null)
                    {
                        fileNameAvata = Path.GetFileName(Avatar.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(AvatarPath, fileNameAvata), FileMode.Create))
                        {
                            Avatar.CopyTo(stream);

                        }
                    }


                    if (model.ID == 0)
                    {

                        #region For Create
                         

                        var product = _mapper.Map<Product>(model);

                        if (!string.IsNullOrEmpty(fileNameAvata))
                        {
                            product.ImagePath = fileNameAvata;
                        }
                        db.Products.Add(product);
                        db.SaveChanges();
                        if (PropertyList != null)
                        {
                            foreach(var item in PropertyList)
                            {
                                db.ProductColorSizes.Add(item);
                            }
                            db.SaveChanges();
                        }

                        #endregion

                    }
                    else
                    {

                        #region for edit
                        var product = db.Products.Find(model.ID);
                  

                        product = _mapper.Map(model, product);

                        if (!string.IsNullOrEmpty(fileNameAvata))
                        {
                            product.ImagePath = fileNameAvata;
                        }

                        db.Update(product);
                        db.SaveChanges();
                        if (PropertyList != null)
                        {
                            foreach (var item in PropertyList)
                            {
                                item.ProductID = model.ID;
                                item.ProductName = db.Products.Find(model.ID).ProductName;
                                item.NameColor = db.CT_Colors.SingleOrDefault(x => x.Code == item.CodeColor).Name;
                                if (item.Id == 0)
                                {
                                    
                                    db.ProductColorSizes.Add(item);
                                }
                                else
                                {
                                    
                                    db.ProductColorSizes.Update(item);
                                }    
                            }
                            db.SaveChanges();
                        }
                        #endregion

                    }
                    var DetailImage = new Image();
                    foreach (var item in DetailImages)
                    {
                        var fileNameTemp = Path.GetFileName(item.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(DetailImagePath, fileNameTemp), FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        if (!string.IsNullOrEmpty(item.FileName))
                        {
                            DetailImage.ImagePath = item.FileName;
                            DetailImage.ReferenceId = model.ID;
                            DetailImage.Type = "Product";
                            DetailImage.IsShow = true;
                            db.Images.Add(DetailImage);
                        }
                    }
                    db.SaveChanges();

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



        public IActionResult NOTFOUND()
        {

            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");


            }


            var product = db.Products.Find(id);


            if (product == null)
            {
                return RedirectToAction("NotFound");

            }

            return View(product);





        }


        [HttpPost]
        public IActionResult DeleteCofirm(Product mode)
        {
            try
            {

                var product = db.Products.Find(mode.ID);

                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();

                    @TempData["StatusMessage"] = "Xóa thanh cong";

                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Delete", mode);

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [HttpPost]
        public JsonResult ImageDelete(int id)
        {
            var image = db.Images.Find(id);
            if (image != null)
            {
                db.Images.Remove(image);
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
        [HttpPost]
        public JsonResult PropertyDelete(int id)
        {
            var property = db.ProductColorSizes.Find(Convert.ToInt64(id));
            if (property != null)
            {
                db.ProductColorSizes.Remove(property);
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

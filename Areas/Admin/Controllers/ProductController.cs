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

            List<Category> lstData = db.Categories.ToList();



            //List<Category> lstData = db.Categories.ToList();
            //var lstcategory = new List<SelectListItem>();
            //var tempItemdf = new SelectListItem();
            //tempItemdf.Value = "".ToString();
            //tempItemdf.Text = "-- Bạn hãy chọn danh mục";
            //lstcategory.Add(tempItemdf);

            //foreach (var item in lstData)
            //{
            //    var tempItem = new SelectListItem();
            //    tempItem.Value = item.CategoryID.ToString();
            //    tempItem.Text = item.CategoryName;

            //    lstcategory.Add(tempItem);
            //}

            //ViewBag.lstcategory = lstcategory;



            if (id > 0)
            {



                var dataProduct = db.Products.Where(x => x.ID == id && x.IsDeleted != true).FirstOrDefault();

                // model.CategoryID = dataProduct.CategoryID != null ? dataProduct.CategoryID.Value : 0;
                //model.ProductID = dataProduct.ProductID;
                //model.ProductName = dataProduct.ProductName;
                model = _mapper.Map<ProductModel>(dataProduct);

                model.CategoryList = new List<SelectListItem>();

                var tempItemdf = new SelectListItem();
                tempItemdf.Value = "".ToString();
                tempItemdf.Text = "-- Bạn hãy chọn danh mục";
                model.CategoryList.Add(tempItemdf);

                foreach (var item in lstData)
                {
                    var tempItem = new SelectListItem();
                    tempItem.Value = item.ID.ToString();
                    tempItem.Text = item.CategoryName;

                    model.CategoryList.Add(tempItem);
                }



            }


            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductModel model, IFormFile avata, List<IFormFile> hinhchitiet)
        {

            try
            {
                string wwwPath = _env.WebRootPath;
                string contentPath = _env.ContentRootPath;

                string path = Path.Combine(this._env.WebRootPath, "Image");//lưu vào 1 Foder =  ~/Images/Product


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }

                foreach (var item in hinhchitiet)
                {
                    var fileNameTemp = Path.GetFileName(item.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileNameTemp), FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }

                    //lưu vào 1 Foder =  ~/Images/Product/ProductDetail
                }

                if (ModelState.IsValid)
                {
                    string fileNameAvata = "";
                    if (avata != null)
                    {
                        fileNameAvata = Path.GetFileName(avata.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(path, fileNameAvata), FileMode.Create))
                        {
                            avata.CopyTo(stream);


                        }
                    }


                    if (model.ID == 0)
                    {

                        #region For Create
                        //var product = new Product
                        //{
                        //    CategoryID = model.CategoryID,
                        //    ProductID = model.ProductID,
                        //    ProductName = model.ProductName,

                        //};

                        var product = _mapper.Map<Product>(model);

                        if (!string.IsNullOrEmpty(fileNameAvata))
                        {
                            product.ImagePath = fileNameAvata;
                        }
                        db.Products.Add(product);
                        db.SaveChanges();
                        #endregion

                    }
                    else
                    {

                        #region for edit
                        var product = db.Products.Find(model.ID);
                        //product.CategoryID = model.CategoryID;
                        //product.ProductID = model.ProductID;
                        //product.ProductName = model.ProductName;

                        product = _mapper.Map(model, product);

                        if (!string.IsNullOrEmpty(fileNameAvata))
                        {
                            product.ImagePath = fileNameAvata;
                        }

                        db.Update(product);
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




    }
}

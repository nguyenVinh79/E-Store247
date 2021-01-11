using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using ShopBanHang.Helper;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private DataShopContext db;
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment _env;
        public CategoryController(IMapper mapper, IConfiguration configuration, DataShopContext dbcontext, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            db = dbcontext;
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            var categoryList = new List<Category>();
            try
            {
                categoryList = db.Categories.Where(x => x.IsDeleted != true).ToList();
            }
            catch(Exception ex)
            {
                TempData["StatusMessage"] = "Error in loanding category list";
            }
            return View(categoryList);
        }
        public IActionResult Create(int id = 0)
        {
            var model = new CategoryModel();
            
            
            if (id > 0)
            {
                
                var CategoryData = db.Categories.Where(x => x.ID == id && x.IsDeleted != true).FirstOrDefault();

                model = _mapper.Map<CategoryModel>(CategoryData);

            }
            #region Load category for dropdownlist
                List<Category> categoryList = db.Categories.ToList();
                model.LstParentCategory = new List<SelectListItem>();

                var temp = new SelectListItem();

                temp.Text = "----Assign parent category role----";
                temp.Value = "0";
                model.LstParentCategory.Add(temp);
                BindTree(categoryList, null, model.LstParentCategory);
            
            #endregion


            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CategoryModel model, IFormFile image)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    
                    
                    if (model.ID == 0)
                    {

                        #region For Create


                        var category = _mapper.Map<Category>(model);
                        if (image != null)
                        {
                            string urlImage = MyTool.UploadImage(image, "wwwroot", "Image", "Categories");
                            if (!string.IsNullOrEmpty(urlImage))
                            {
                                category.Image = urlImage;
                            }
                        }
                        
                        db.Categories.Add(category);
                        db.SaveChanges();

                        #endregion

                    }
                    else
                    {

                        #region for edit
                        //var categoryEdit = db.Categories.SingleOrDefault(x=> x.ID == model.ID);
                        //Require make a new instane of Category ty
                        var categoryEdit = _mapper.Map<Category>(model);
                        //categoryEdit = _mapper.Map(model, categoryEdit);
                        if (image != null)
                        {
                            string urlImage = MyTool.UploadImage(image, "wwwroot", "Image", "Categories");
                            if (!string.IsNullOrEmpty(urlImage))
                            {
                                categoryEdit.Image = urlImage;
                            }
                        }

                        db.Update(categoryEdit);
                        db.SaveChanges();
                        
                        #endregion

                    }
                    
                    return RedirectToAction("Index");
                }
            
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = ex.Message;
                #region Load category for dropdownlist
                List<Category> cateList = db.Categories.ToList();
                model.LstParentCategory = new List<SelectListItem>();

                var tempItem = new SelectListItem();

                tempItem.Text = "----Assign parent category role----";
                tempItem.Value = "0";
                model.LstParentCategory.Add(tempItem);
                BindTree(cateList, null, model.LstParentCategory);

                #endregion
                return View(model);
            }
            #region Load category for dropdownlist
            List<Category> categoryList = db.Categories.ToList();
            model.LstParentCategory = new List<SelectListItem>();

            var temp = new SelectListItem();

            temp.Text = "----Assign parent category role----";
            temp.Value = "0";
            model.LstParentCategory.Add(temp);
            BindTree(categoryList, null, model.LstParentCategory);

            #endregion
            return View(model);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category != null)
            {
                var productsOfCate = db.Products.Where(x => x.CategoryID == id).ToList();
                if (productsOfCate.Count > 0)
                {
                    return Json(new
                    {
                        status = false,
                        message = "One or more product has been used this category!"
                    });
                }
                else
                {
                    db.Categories.Remove(category);
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
        public void BindTree(IEnumerable<Category> list, SelectListItem parentItem, List<SelectListItem> listParent)
        {
            string i = "";
            i = i + "--";
            var nodes = list.Where(x => parentItem == null ? x.ParentCategoryID == 0 : x.ParentCategoryID == int.Parse(parentItem.Value)).ToList();

            foreach (var node in nodes)
            {
                if (node.Equals(nodes[nodes.Count - 1]) && node.ParentCategoryID != 0)
                {
                    i = i + "--";
                }
                SelectListItem newNode = new SelectListItem(node.CategoryName, node.ID.ToString());
                if (parentItem == null)
                {
                    listParent.Add(newNode);
                }
                else
                {
                    listParent.Add(new SelectListItem($"{i} {newNode.Text}", newNode.Value));
                }

                BindTree(list, newNode, listParent);


            }
        }
    }
}

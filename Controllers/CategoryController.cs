using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopBanHang.Models;

namespace ShopBanHang.Controllers
{
    public class CategoryController : BaseController
    {

        private readonly DataShopContext _db;
        public CategoryController(DataShopContext dbcontext)
        {
            _db = dbcontext;

        }
        public IActionResult Index()
        {
            //chỉ show danh sách sản phẩm, CategoryInfo sẽ bằng null
            var datamodel = new CategoryModelView();

            datamodel.DataList = _db.Products.Where(x => x.Published == true && x.IsDeleted != true).ToList();


            return View(datamodel);
        }
        [Route("c-{title}-{id}")]
        public IActionResult DetailByID (int id)
        {
            CategoryModelView datamodel = new CategoryModelView();
            List<Category> categoryChild = _db.Categories.Where(x => x.ParentCategoryID == id).ToList();
            if (categoryChild != null && categoryChild.Count >0)
            {
                var listIDCate = categoryChild.Select(x => x.ID).ToList();
                //x.CategoryID có thể null do int?, .value để không bị null
                datamodel.DataList = _db.Products.Where( x => ( listIDCate.Contains(x.CategoryID.Value)|| x.CategoryID == id) && x.Published == true && x.IsDeleted != true).ToList();


            }
            else
            {
                datamodel.DataList = _db.Products.Where(x => x.CategoryID == id && x.Published == true && x.IsDeleted != true).ToList();
            }
            datamodel.CategoryInfo = _db.Categories.Find(id);

            return View("Index", datamodel);
        }
    }
}

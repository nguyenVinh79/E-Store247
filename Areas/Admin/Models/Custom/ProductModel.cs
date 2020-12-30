using Microsoft.AspNetCore.Mvc.Rendering;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LayoutLesson2.Areas.Admin.Models
{
    public class ProductModel
    {
        private readonly DataShopContext _db;
        public int ProductID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "Can't enter exceed 100 characters")]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(1000000, 50000000, ErrorMessage = "Enter number between 1.000.000 and 50.000.000")]
        public Nullable<Double> UnitPrice { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Range(1000000, 50000000, ErrorMessage = "Enter number between 1.000.000 and 50.000.000")]
        public Nullable<Double> UnitPriceNew { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public Nullable<int> CategoryID { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
     
        public Nullable<bool> Published { get; set; }
        public Nullable<bool> IsNew { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+")]
        public string Code { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public List<SelectListItem> LstCategory { get; set; }

        public ProductModel( DataShopContext dbcontext)
        {
            _db = dbcontext;
        }
        public ProductModel()
        {
      
            //gọi xuống db lấy data
            
                List<Category> lstData = _db.Categories.Where(x => x.ParentCategoryID == 0).ToList();
                
                //khởi tạo List<>  khai báo dữ liệu
                this.LstCategory = new List<SelectListItem>();

                var temp2 = new SelectListItem();
                //thêm data rỗng để mặc định chọn là rỗng
                temp2.Text = "----Please select the category of new product----";
                temp2.Value = "0";
                this.LstCategory.Add(temp2);
                int i = 1;
                foreach (var item in lstData)
                {
                   
                    var temp = new SelectListItem();

                    temp.Text = i.ToString()+ ". " + item.CategoryName;
                    temp.Value = item.ID.ToString();

                    this.LstCategory.Add(temp);
                    int j = 1;
                    List<Category> lstCateChild = _db.Categories.Where(x => x.ParentCategoryID == item.ID).ToList();
                    foreach (var itemChild in lstCateChild)
                    {

                        var temp1 = new SelectListItem();

                        temp1.Text = "--"+ i.ToString()+"." +j.ToString()+". " + itemChild.CategoryName;
                        temp1.Value =  itemChild.ID.ToString();

                        this.LstCategory.Add(temp1);
                        j++;
                    }
                    i++;
                }
            
        }
    }
}

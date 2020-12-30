using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LayoutLesson2.Areas.Admin.Models
{
    public class CategoryModel
    {
        private readonly DataShopContext _db;
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "Can't enter exceed 100 characters")]
        public string CategoryName { get; set;}
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select it's parent category")]
        public Nullable<int> ParenCategoryID { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string Class { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string LanguageID { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<bool> ShowIndex { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public List<SelectListItem> LstParentCategory { get; set; }

        public CategoryModel(DataShopContext dbcontext)
        {
            _db = dbcontext;
        }
        public CategoryModel()
        {
            //gọi xuống db lấy data
            
                List<Category> lstData = _db.Categories.ToList();
                //khởi tạo List<>  khai báo dữ liệu
                this.LstParentCategory = new List<SelectListItem>();

                var temp = new SelectListItem();
                //thêm data rỗng để mặc định chọn là rỗng
                temp.Text = "----assign parent category role----";
                temp.Value = "0";
                this.LstParentCategory.Add(temp);
                //int i = 1;
                //foreach (var item in lstData)
                //{
                //    var temp1 = new SelectListItem();

                //    temp1.Text = i.ToString() + ". " + item.CategoryName;
                //    temp1.Value = item.CategoryID.ToString();

                //    this.LstParentCategory.Add(temp1);
                //    i++;
                //}
                BindTree(lstData, null , this.LstParentCategory);
            
        }
        private void BindTree(IEnumerable<Category> list, SelectListItem parentItem, List<SelectListItem> listParent)
        {
            string i="";
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

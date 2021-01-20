using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Models
{
    
   
    public class Category
    {
    
      
        public int  ID { get; set; }
      
        public string  CategoryName { get; set; }
      
        public string Description { get; set; }
      
        public int? ParentCategoryID { get; set; }
      
        public string MetaKeywords { get; set; }
      
        public string MetaDescription { get; set; }
      
        public string MetaTitle { get; set; }
      
        public string Class { get; set; }
      
        public string Image { get; set; }
      
        public bool? IsDeleted { get; set; }   

        public int? Order { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new HashSet<Product>(); 
        }

    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Models
{
 


    public class CategoryModelView
    {

        public Category CategoryInfo { get; set; }
        public List<Product> DataList { get; set; }
 


    }
}

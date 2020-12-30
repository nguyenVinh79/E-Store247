using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Models
{
   public class ProductViewModel
    {
        public Product ProductInfo { get; set; }
        public List<Image> ImageList { get; set; }
        public List<Relate> RelateList { get; set; }
        public List<ProductColorSize> PropertyList { get; set; }


    }


    
}

using System.Collections.Generic;

namespace ShopBanHang.Models
{
    public class CategoryModelView
    {
        public Category CategoryInfo { get; set; }
        public List<Product> DataList { get; set; }
    }
}
using System.Collections.Generic;

namespace ECommerce.Project.Models
{
    public class ProductViewModel
    {
        public Product ProductInfo { get; set; }
        public List<Image> ImageList { get; set; }
        public List<Relate> RelateList { get; set; }
        public List<ProductColorSize> PropertyList { get; set; }
    }
}
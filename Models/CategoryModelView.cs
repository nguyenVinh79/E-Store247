using System.Collections.Generic;

namespace ECommerce.Project.Models
{
    public class CategoryModelView
    {
        public Category CategoryInfo { get; set; }
        public List<Product> DataList { get; set; }
    }
}
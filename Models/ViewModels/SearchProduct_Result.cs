using System;

namespace ECommerce.Project.Models
{
    public class SearchProduct_Result
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> UnitPriceNew { get; set; }
        public int TotalCount { get; set; }
    }
}
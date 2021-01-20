using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Project.Models
{
    public class ProductColorSize
    {
        public long Id { get; set; }
        public int ColorId { get; set; }
        public string CodeColor { get; set; }
        public string NameColor { get; set; }
        public string CodeSize { get; set; } 
        public string NameSize { get; set; } 
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Quantity_Saled { get; set; }
        public double UnitPrice { get; set; }
        public double UnitPriceNew { get; set; } 
        public Nullable<double> UnitPriceNewOld { get; set; } 
        public Nullable<int> ProductID { get; set; }
        [IgnoreDataMember]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public string ProductName { get; set; }
        [ForeignKey("ColorId")]
        public virtual CT_Color CT_Color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ShopBanHang.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string ProductName { get; set; }

        public string Code { get; set; }

        public string ShortDecription { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public double? UnitPrice { get; set; }

        public double UnitPriceNew { get; set; }

        public string CategoryName { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public bool? Published { get; set; }

        public bool? IsNew { get; set; }

        public int? Rating { get; set; }

        public int? Quantity { get; set; }

        public string URL { get; set; }

        public int? SupplierID { get; set; }
        public string SupplierName { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CT_WarrantyTimeID { get; set; }

        public string CT_WarrantyTimeName { get; set; }

        public string SizeName { get; set; }

        public int? SizeID { get; set; }
        public int? CT_MaterialID { get; set; }
        public string CT_MaterialName { get; set; }

        public string Video { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }

        public int? CategoryID { get; set; }

        [JsonIgnore]
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ProductColorSize> ProductColorSizes { get; set; }

        public Product()
        {
            ProductColorSizes = new HashSet<ProductColorSize>();
        }
    }
}
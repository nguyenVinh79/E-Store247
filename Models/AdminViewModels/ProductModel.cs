using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Project.Models
{
    public class ProductModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter product name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter product code")]
        public string Code { get; set; }

        public string ShortDecription { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Range(0, 100000000, ErrorMessage = "Enter number between 0 to 100.000.000")]
        [DataType(DataType.Text)]
        public double? UnitPrice { get; set; }

        [Range(0, 100000000, ErrorMessage = "Enter number between 0 to 100.000.000")]
        public double? UnitPriceNew { get; set; }

        [Required]
        public int? CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public bool Published { get; set; }

        public bool IsNew { get; set; }

        public int? Rating { get; set; }

        public int? Quantity { get; set; }

        public string URL { get; set; }

        public int? SupplierID { get; set; }

        public string SupplierName { get; set; }

        public DateTime DateCreated { get; set; }

        public bool? IsDelected { get; set; }

        public int? CT_WarrantyTimeID { get; set; }

        public string CT_WarrantyTimeName { get; set; }

        public int? CT_MaterialID { get; set; }

        public string CT_MaterialName { get; set; }

        public string SizeName { get; set; }

        public int? SizeID { get; set; }

        public string UpdateBy { get; set; }

        public string Video { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateLBy { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public List<Image> DetailImageList { get; set; }
    }
}
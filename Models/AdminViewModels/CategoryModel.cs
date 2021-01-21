using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ECommerce.Project.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "Can't enter exceed 100 characters")]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please select it's parent category")]
        public Nullable<int> ParentCategoryID { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string Class { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public string LanguageID { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<bool> ShowIndex { get; set; }
        public Nullable<int> Order { get; set; }
        public List<SelectListItem> LstParentCategory { get; set; }

        public CategoryModel()
        {
          
        }

    }
}
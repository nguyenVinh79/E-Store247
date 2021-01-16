using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Areas.Admin.Models
{
   
    public class SettingModel
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Code { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public int SettingType { get; set; }
        
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
}

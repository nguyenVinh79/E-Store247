using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Areas.Admin.Models
{
    public class ColorModel
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(15)]
        public string Code { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        public bool Active { get; set; }
        public string CSS { get; set; }
    }
}

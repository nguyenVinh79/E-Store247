using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Areas.Admin.Models
{
    public class SupplierVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string LinkTo { get; set; }
    }
}

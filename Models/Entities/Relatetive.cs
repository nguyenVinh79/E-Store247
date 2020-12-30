using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Models
{
    public class Relate
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public long IDRelate { get; set; }
        public string TypeRelate { get; set; } 
        public string Name { get; set; }
        public Nullable<int> OrderNumber { get; set; }

    }
}

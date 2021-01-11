using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Models
{
    public class ProductColorSize
    {
        public long Id { get; set; }
        public string CodeColor { get; set; } //Mã màu
        public string NameColor { get; set; } // Vang , Vàng
        public string CodeSize { get; set; } // L 
        public string NameSize { get; set; } // size large,...
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Quantity_Saled { get; set; }
        public double UnitPrice { get; set; } // giá ban đầu
        public double UnitPriceNew { get; set; } // giá hiện tại
        public Nullable<double> UnitPriceNewOld { get; set; } // giá hiện tại trước khi km
        public Nullable<int> ProductID { get; set; } 
        public string ProductName { get; set; }
    }
}

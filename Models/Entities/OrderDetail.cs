using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBanHang.Models
{
    public class OrderDetail
    {
        [Key]
        public long OrderDetailId { get; set; }

        public long OrderId { get; set; }

        public int ProductId { get; set; }
        //public string ProductName { get; set; }
        public int Quantity { get; set; }

        public double? UnitPrice { get; set; }

        public string ColorSize { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
}
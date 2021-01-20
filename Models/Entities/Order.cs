using System;
using System.Collections.Generic;

namespace ShopBanHang.Models
{
    public class Order
    {
        public long OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public int? WardID { get; set; }

        public double? Total { get; set; }

        public double? ShipFee { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
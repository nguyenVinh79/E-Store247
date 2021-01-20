using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Project.Models
{
    public class OrderDetail
    {
        [Key]
        public long OrderDetailId { get; set; }

        public long OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public double? UnitPrice { get; set; }

        public string ColorSize { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
}
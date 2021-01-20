using System;
using System.Collections.Generic;

namespace ShopBanHang.Models
{
    public partial class Supplier
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LinkTo { get; set; }

        public int? OrderBy { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Supplier()
        {
            Products = new HashSet<Product>();
        }
    }
}
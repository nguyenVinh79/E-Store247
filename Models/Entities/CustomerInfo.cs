using System;

namespace ShopBanHang.Models
{
    public class CustomerInfo
    {
        public long CustomerInfoID { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public int? WardID { get; set; }

        public string Avatar { get; set; }

        public string Note { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
}
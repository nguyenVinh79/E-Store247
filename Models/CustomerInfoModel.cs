using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBanHang.Models
{
    public class CustomerInfoModel
    {
        public long CustomerInfoID { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public int? WardID { get; set; }

        public string Avatar { get; set; }

        public string Note { get; set; }

        public double Total { get; set; }

        public double ShipFee { get; set; }


    }
}

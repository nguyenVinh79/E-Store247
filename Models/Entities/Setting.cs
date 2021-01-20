using System;

namespace ShopBanHang.Models
{
    public class Setting
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public int SettingType { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
}
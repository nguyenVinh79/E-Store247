using System;

namespace ShopBanHang.Models
{
    public class Image
    {
        public long Id { get; set; }
        public string ImagePath { get; set; }
        public Nullable<int> ReferenceId { get; set; }
        public string Type { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public string ImgName { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
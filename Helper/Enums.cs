using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopBanHang.Helper
{
    public class Enums
    {
        public enum SiderType : short
        {
            Sider_Home = 1,
             
        }

        public enum SettingTypeData  
        {
            [Display(Name ="HTML")] HTML = 1,
            [Display(Name = "FILE")] FILE = 2,
            [Display(Name = "TEXT")] TEXT = 3,
        }

        public enum BannerType
        {
            [Display(Name = "Slider")] Slider = 1,
            [Display(Name = "FILE")] Banner = 2
        }


    }
}
